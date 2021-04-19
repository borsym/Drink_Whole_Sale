using DrinkWholeSale.Web.Models;
using DrinkWholeSale.Web.Models.Shopping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Services
{
    public class DrinkWholeSaleService : IDrinkWholeSaleService
    {
        private readonly DrinkWholeSaleDbContext _context;
        private readonly UserManager<Guest> _userManager;
        public static Packaging getPacking(int quant)
        {
            if (quant >= 6 && quant < 12)
                return Packaging.SHRINK_FILM;
            if (quant >= 12 && quant < 24)
                return Packaging.SALVER;
            if (quant >= 24)
                return Packaging.TRAY;
            return Packaging.PIECE;
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
        public DrinkWholeSaleService(DrinkWholeSaleDbContext context, UserManager<Guest> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //---MAINCAT---
        // visszaadja a főkategóriákat név szerint szürhetünk ha akarunk
        public IEnumerable<MainCat> MainCats => _context.MainCats; // ez nemtudom kell e mert lazyloadingot használok
      //  public IEnumerable<ShoppingCart> ShoppingCarts => _context.ShoppingCarts;
        public List<MainCat> GetMainCats(String name = null)
        {
            return _context.MainCats
                .Where(l => l.Name.Contains(name ?? ""))
                .OrderBy(l => l.Name)
                .ToList();
        }


        public async Task<bool> AddOrder(List<ShoppingCart> list, string userName)
        {
            
            Guest guest = await _userManager.FindByNameAsync(userName);
            if (guest == null)
            {
                return false;
            }
            List<Product> my_list = new List<Product>();
            foreach (var item in list)
            {
                var product = _context.Products.FirstOrDefault(i => i.Id == item.ProductId);
                if(product != null)
                {
                    if (item.Pack > product.Pack)  // mivel számozva vannak ezért van növekvő sorrend
                    {
                        return false;
                    }
                    // pieace 0 , shrink 1 salver 2 try 3
                    if (product.Quantity < item.Quantity)
                    {
                        return false;
                    }
                }
            }

            _context.Orders.Add(new Order  
            {
                Address = guest.Address,
                Email = guest.Email,
                Guest = guest,
                Name = guest.Name,
                items = list,  
                fulfilled = true,
                GuestId = guest.Id,
                Phone =  guest.PhoneNumber,
            });
          

            try
            {
                foreach (var item in list)
                {
                    var product = _context.Products.FirstOrDefault(i => i.Id == item.ProductId);
                    product.Quantity = product.Quantity - PackNumber(item.Pack, item.Quantity);
                    product.Pack = getPacking(product.Quantity);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            return true;
            

        }
        public MainCat GetMainCatById(int? id)
        {
            return _context.MainCats
                .FirstOrDefault(l => l.Id == id);  // nincs talalat akkor null, legbiztonságosabb
        }
        
        public List<SubCat> GetProductByMainCatId(int id)  
        {
            return _context.MainCats
                .Include(l => l.SubCats)
                .Single(l => l.Id == id)
                .SubCats.ToList();
        }

        public MainCat GetMainCatDetails(int? id) // itt nem biztos hogy ilyen mélyre kell menni?
        {
            return _context.MainCats
                .Include(l => l.SubCats).
                ThenInclude(i => i.Products)
                .Single(l => l.Id == id);
        }

        public MainCat EditMainCat(int? id)
        {
            return _context.MainCats.Find(id);
        }

        public bool RemoveMainCat(MainCat mainCat)
        {
            try
            {
                _context.MainCats.Remove(mainCat);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }
        public bool IsMainCatExists(int? id)
        {
            return _context.MainCats.Any(e => e.Id == id);
        }
        public List<SubCat> GetSubCatsByMainCatId(int id)
        {
            return _context.MainCats
                .Include(s => s.SubCats)
                .Single(l => l.Id == id)
                .SubCats.ToList();
        }
        public bool AddMainCat(MainCat mainCat)
        {
            try
            {
                _context.Add(mainCat);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return false;
            }
            return true;
        }

        public bool UpdateMainCat(MainCat mainCat)
        {
            _context.Update(mainCat);
            _context.SaveChanges();
            return true;
        }
        // ---SUBCAT---
        // visszaadja az alkategóriákat név szerint szürhetünk ha akarunk

        public List<SubCat> GetSubCats(String name = null)
        {
            return _context.SubCats
                .Where(l => l.Name.Contains(name ?? ""))
                .OrderBy(l => l.Name)
                .ToList();
        }
        
        public SubCat GetSubCatById(int id)
        {
            return _context.SubCats
                .FirstOrDefault(l => l.Id == id);  // nincs talalat akkor null, legbiztonságosabb
        }
        public IEnumerable<SubCat> SubCats => _context.SubCats.Include(b => b.MainCat);

        //---PRODUCT---

        public List<Product> GetProductBySubCatId(int id)  // itt esetleg lehet egy olyan, hogy maincategóriából jövök és include, include then
        {
            return _context.SubCats
                .Include(l => l.Products)
                .Single(l => l.Id == id)
                .Products.ToList();
        }
        public Product GetProductById(int? id)
        {
            if (id == null)
                return null;

            return _context.Products
                .Include(a => a.SubCat) // betöltjük az apartmanhoz az épületeket
                .ThenInclude(b => b.MainCat) // az épülethez pedig a várost
                .FirstOrDefault(prod => prod.Id == id);

        }

        

        //---EGYÉB---
        //public void AddItemShoppingCart(ShoppingCart cartItem)
        //{
        //    _context.ShoppingCarts.Add(cartItem);
        //    _context.SaveChanges();
        //}
        //public void RemoveItemShoppingCart(ShoppingCart cartItem)
        //{
        //    _context.ShoppingCarts.Remove(cartItem);
        //    _context.SaveChanges();
        //}
        public ShoppingCart GetShoppingCartProduct(int id)
        {
            return new ShoppingCart();//_context.ShoppingCarts.Single(p => p.ProductId == id);
        }

        //public ShoppingCart GetShoppingCartById(int id)
        //{
        //    return _context.ShoppingCarts.FirstOrDefault(i => i.Id == id);
        //}

        public ShoppingCartViewModel newShoppingCart(int? productId)
        {
            if (productId == null)
                return null;

            Product product = _context.Products
                .Include(a => a.SubCat) 
                .ThenInclude(b => b.MainCat) 
                .FirstOrDefault(ap => ap.Id == productId);

            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel { Product = product}; // létrehozunk egy új foglalást, amelynek megadjuk az terméket

            // beállítunk egy foglalást, amely a következő megfelelő fordulónappal (minimum 1 héttel később), és egy hetes időtartammal rendelkezik
            //rent.RentStartDate = DateTime.Today + TimeSpan.FromDays(7);
            //while (rent.RentStartDate.DayOfWeek != apartment.Turnday)
            //    rent.RentStartDate += TimeSpan.FromDays(1);

            //rent.RentEndDate = rent.RentStartDate + TimeSpan.FromDays(7);
            
            // a itt be lett állítva az időintervallum amit lefoglal, akkor itt kellene nekem levonni a darabszámot?

            return shoppingCart;
        }
        public AddShoppingCartViewModel newShoppingCartAdd(int? productId)
        {
            if (productId == null)
                return null;

            Product product = _context.Products
                .Include(a => a.SubCat)
                .ThenInclude(b => b.MainCat)
                .FirstOrDefault(ap => ap.Id == productId);

            AddShoppingCartViewModel shoppingCart = new AddShoppingCartViewModel { Product = product }; // létrehozunk egy új foglalást, amelynek megadjuk az terméket


            return shoppingCart;
        }
        public async Task<bool> SaveShoppingCartAsync(int? productId,string userName, ShoppingCartViewModel cart)
        {
            if (productId == null || cart == null) return false;

            if (!Validator.TryValidateObject(cart, new ValidationContext(cart, null, null), null))
                return false;
            Guest guest = await _userManager.FindByNameAsync(userName);

            if (guest == null)
                return false;

            //_context.ShoppingCarts.Add(new ShoppingCart
            //{
            //    ProductId = cart.Product.Id,
            //  //  UserId = guest.Id, // átírtam a UserId-t stringre
            //    Quantity = cart.Product.Quantity // ez igy talan jo
            //});

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                // mentéskor lehet hiba
                return false;
            }

            // ha idáig eljutottunk, minden sikeres volt
            return true;
        }
        public int GetPrice(int? productId, ShoppingCartViewModel cart)
        {
            if (productId == null || cart == null || cart.Product == null) // itt van a baj nem csak product hanem több product kéne
                return 0;
            return cart.Product.NetPrice;
        }
        public int GetGrossPrice(int? productId, ShoppingCartViewModel cart)
        {
            if (productId == null || cart == null || cart.Product == null) // itt van a baj nem csak product hanem több product kéne
                return 0;
            return cart.Product.GrossPrice;
        }
    }
}
