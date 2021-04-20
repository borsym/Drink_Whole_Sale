using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DrinkWholeSale.Persistence;
using Microsoft.AspNetCore.Mvc.Filters;
using DrinkWholeSale.Persistence.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DrinkWholeSale.Web.Controllers
{
    public class ShoppingCartsController : BaseController
    {
        private readonly UserManager<Guest> _userManager;

        public ShoppingCartsController(IDrinkWholeSaleService service, ApplicationState applicationState,
            UserManager<Guest> userManager)
            : base(applicationState, service)
        {
            _userManager = userManager;
        }
        public bool isValidBuy(Packaging Pack, int quantCar, int quantProd)
        {
            switch (Pack)
            {
                case Packaging.PIECE:
                    return true;
                case Packaging.SHRINK_FILM:
                    if (quantCar * 6 <= quantProd) return true;
                    else return false;
                case Packaging.SALVER:
                    if (quantCar * 12 <= quantProd) return true;
                    else return false;
                case Packaging.TRAY:
                    if (quantCar * 24 <= quantProd) return true;
                    else return false;
            }
            return true;
        }
        private int PackNumber(Packaging pack, int quantCar)
        {
            switch (pack)
            {
                case Packaging.PIECE:
                    return 1;
                case Packaging.SHRINK_FILM:
                    return quantCar * 6;
                case Packaging.SALVER:
                    return quantCar * 12;
                case Packaging.TRAY:
                    return quantCar * 24;
            }
            return 0;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Shopping = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
            if (Shopping == null) return View();
            var totalGrossPrice = 0;
            // ha ez null akkor nem kell lefuttatni
            foreach (var item in Shopping)
            {
                totalGrossPrice += item.TotalGrossPrice;
            }
            ViewBag.totalGrossPrice = totalGrossPrice;

            ShoppingCartViewModel cart = new ShoppingCartViewModel();
            
            if (User.Identity.IsAuthenticated)
            {
                Guest guest = await _userManager.FindByNameAsync(User.Identity.Name);

                // akkor az adatait közvetlenül is betölthetjük
                if (guest != null)
                {
                    cart.GuestAddress = guest.Address;
                    cart.GuestEmail = guest.Email;
                    cart.GuestName = guest.Name;
                    cart.GuestPhoneNumber = guest.PhoneNumber;
                }
                // így nem kell újra megadnia
            }
        
            return View("Index",cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(/*int? id,*/ ShoppingCartViewModel cart)
        {
           
            Debug.WriteLine(ModelState.ErrorCount);
            if (!ModelState.IsValid)  // ha nincs megadva egy requiered mezo akkor lehal
                return View("Index", cart);

            Guest guest;
            // bejelentkezett felhasználó esetén nem kell felvennünk az új felhasználót

            if (User.Identity.IsAuthenticated)
            {
                guest = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                guest = new Guest
                {
                    
                    UserName = "user" + Guid.NewGuid(),
                    Email = cart.GuestEmail,
                    Name = cart.GuestName,
                    Address = cart.GuestAddress,
                    PhoneNumber = cart.GuestPhoneNumber
                };
                var result = await _userManager.CreateAsync(guest); // a felhasználónak nem lesz (kezdetben) jelszava
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "A vásárls rögzítése sikertelen, kérem próbálja újra!");
                    return View("Index", cart);
                }
            }

          


            var cart_session = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
            foreach (var item in cart_session)
            {
                cart.TotalPrice += item.TotalGrossPrice;
            }

          
            if (!await _service.AddOrder(cart_session, guest.UserName))
            {
                ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
                return View("Index");
            }


            ViewBag.Message = "Sikeres vásárlás";
            HttpContext.Session.Set < List<ShoppingCart>>("CartSessionKey", new List<ShoppingCart>());
            return View("Result", cart);
        }

        public IActionResult Main()
        {
            var Shopping = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
            if (Shopping == null) return View();
            var totalNetPrice = 0;
            var totalGrossPrice = 0;
            // ha ez null akkor nem kell lefuttatni
            foreach (var item in Shopping)
            {
                totalNetPrice += item.TotalPrice;
                totalGrossPrice += item.TotalGrossPrice;
            }
            ViewBag.TotalNetPrice = totalNetPrice;
            ViewBag.totalGrossPrice = totalGrossPrice;
            ViewBag.CountItems = Shopping.Count();
            Debug.WriteLine(Shopping);
            return View(Shopping);
        }
        // ide kell egy get és egy post 
        [HttpGet]
        public IActionResult AddCart(int? id)
        {
            var cart = _service.newShoppingCartAdd2(id);
            ViewData["Pack"] = new SelectList(Packaging.GetValues(typeof(Packaging)).Cast<Packaging>());
            ViewBag.First = ViewData["Pack"];
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCart(int? id, ShoppingCart cartvm) // ez a product id
        {
            if (id == null || cartvm == null)
                return RedirectToAction("Index", "Home");
           
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                foreach(var i in errors)
                {
                    Debug.WriteLine(i.ToString());
                }
                return View("AddCart", cartvm);
            }
           
            var product = _service.GetProductById(id);
            cartvm.Product = product;
            if(cartvm.Quantity == 0)
            {
                TempData["shortMessage"] = "You can't buy 0 item";
                return RedirectToAction("AddCart", cartvm);
            }
            // hozzadva
            if (cartvm.Pack > product.Pack)  // mivel számozva vannak ezért van növekvő sorrend
            {
                TempData["shortMessage"] = "The Package not aviable";
                return RedirectToAction("AddCart", cartvm);
            } 
            // pieace 0 , shrink 1 salver 2 try 3
            if (product.Quantity < cartvm.Quantity)
            {  
                TempData["shortMessage"] = "Not enough in stock";
                return RedirectToAction("AddCart",cartvm);
            }

            if (!isValidBuy(cartvm.Pack, cartvm.Quantity, product.Quantity))
            {
                TempData["shortMessage"] = "Not enough in stock";
                return RedirectToAction("AddCart", cartvm);
            }
            //megnézem hogy van e már a kosárban, ha igen akkor más pack nem lehet benne
            if (HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey") != default) 
            {
                var items = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
                foreach(var item in items)
                {
                    if(item.ProductId == product.Id)
                    {
                        if(item.Pack != cartvm.Pack)
                        {
                            TempData["shortMessage"] = "You can't put different package into your cart";
                             return RedirectToAction("AddCart", cartvm);
                        }
                    }
                }
            }

            if (HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey") == default)
            {
                List<ShoppingCart> p = new List<ShoppingCart>();
                ShoppingCart cart = new ShoppingCart()
                {
                    Quantity = cartvm.Quantity,
                    Name = product.Name,
                    ProductId = product.Id,// a sima idt növelni kell?
                    //Id = product.Id,
                    Pack = cartvm.Pack,
                    TotalPrice = product.NetPrice * cartvm.Quantity * PackNumber(cartvm.Pack, cartvm.Quantity), // felhasználótól bekért adat kell,
                    TotalGrossPrice = product.GrossPrice * cartvm.Quantity * PackNumber(cartvm.Pack, cartvm.Quantity)
                };
                p.Add(cart);
                HttpContext.Session.Set<List<ShoppingCart>>("CartSessionKey", p);
            }
            else
            {
                var tmp = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");  // itt list kéne hogy hozzáadom
                ShoppingCart cart = new ShoppingCart()
                {
                    Quantity = cartvm.Quantity,
                    Name = product.Name,
                    ProductId = product.Id,
                    //Id = product.Id,
                    Pack = cartvm.Pack,
                    TotalPrice = product.NetPrice * cartvm.Quantity * PackNumber(cartvm.Pack, cartvm.Quantity), // itt a felhasználótól bekért adat kell
                    TotalGrossPrice = product.GrossPrice * cartvm.Quantity * PackNumber(cartvm.Pack, cartvm.Quantity)
                };
                tmp.Add(cart);
                HttpContext.Session.Set <List <ShoppingCart>> ("CartSessionKey", tmp);
            }



            Debug.WriteLine(cartvm.Pack);
            ViewData["Pack"] = new SelectList(Packaging.GetValues(typeof(Packaging)).Cast<Packaging>());
            return RedirectToAction("Main");
        }

        

        public IActionResult Delete(int? ProductId)
        {
            if (ProductId == null)
            {
                return NotFound();
            }

            var ShoppingCart = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
            var product = new ShoppingCart();
           
        
            foreach (var item in ShoppingCart)
            {
                //Debug.WriteLine(item.Id);
                if (item.ProductId == ProductId)
                {
                    Debug.WriteLine(item);
                    product = item;
                }
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ProductId)
        {
            var ShoppingCart = HttpContext.Session.Get<List<ShoppingCart>>("CartSessionKey");
            List<ShoppingCart> p = new List<ShoppingCart>();
            foreach (var item in ShoppingCart)
            {
                if (item.ProductId != ProductId)
                {
                    Debug.WriteLine(item);
                    p.Add(item);
                }
                else
                {
                    Debug.WriteLine("ez kell nekem");
                    Debug.WriteLine(item);
                }
            }

            HttpContext.Session.Set<List<ShoppingCart>>("CartSessionKey", p);
            return RedirectToAction("Main");
        }

        public IActionResult DeleteAll()
        {
            HttpContext.Session.Set<List<ShoppingCart>>("CartSessionKey", new List<ShoppingCart>());
            return RedirectToAction("Main");
        }
    }
}
