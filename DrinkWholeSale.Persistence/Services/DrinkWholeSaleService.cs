using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.Shopping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence.Services
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

        public int PackNumber(Packaging pack, int quantCar)
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

        public DrinkWholeSaleService(DrinkWholeSaleDbContext context)
        {
            this._context = context;
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

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Include(m => m.items).FirstOrDefault(o => o.Id == id);
        }



        public bool DeleteMainCat(int id)
        {
            var list = _context.MainCats.Find(id);
            if (list == null)
            {
                return false;
            }

            try
            {
                _context.Remove(list);
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

        public MainCat CreateMainCat(MainCat mainCat)
        {
            try
            {
                _context.Add(mainCat);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return mainCat;
        }
        public bool OrderExist(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public async Task<bool> SetStateOrderAsync(Order order)
        {
            try
            {
                foreach (var item in order.items)
                {
                    var product = _context.Products.FirstOrDefault(i => i.Id == item.ProductId);
                    if (order.fulfilled)
                    {
                        product.Quantity = product.Quantity + PackNumber(item.Pack, item.Quantity);
                        order.fulfilled = false;
                    }
                    else
                    {
                        if (product.Quantity >= item.Quantity)
                        {
                            product.Quantity = product.Quantity - PackNumber(item.Pack, item.Quantity);
                            order.fulfilled = true;
                        }
                        else return false;

                    }
                    product.Pack = getPacking(product.Quantity);


                }

                var o = _context.Orders.FirstOrDefault(r => r.Id == order.Id);
                o.fulfilled = order.fulfilled;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
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
                if (product != null)
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
                Phone = guest.PhoneNumber,
                orderDate = DateTime.Now
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
            catch (DbUpdateException)
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

        public bool UpdateSubCat(SubCat subCat)
        {
            try
            {
                _context.Update(subCat);
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
        public SubCat CreateSubCat(SubCat subCatdto)
        {
            try
            {
                _context.Add(subCatdto);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return subCatdto;
        }

        public bool DeleteSubCat(int id)
        {
            var item = _context.SubCats.Find(id);
            if (item == null)
            {
                return false;
            }

            try
            {
                _context.Remove(item);
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

        public bool UpdateItem(Product product)
        {
            try
            {
                product.Pack = getPacking(product.Quantity);
                _context.Update(product);
                if (product.Image == null)
                {
                    _context.Entry(product).Property("Image").IsModified = false;
                }
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
        //---PRODUCT---
        public Product CreateProduct(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return product;
        }

        public bool DeleteProduct(int id)
        {
            var item = _context.Products.Find(id);
            if (item == null)
                return false;

            try
            {
                _context.Remove(item);
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





        public ShoppingCart newShoppingCartAdd2(int? productId)
        {
            if (productId == null)
                return null;

            Product product = _context.Products
                .Include(a => a.SubCat)
                .ThenInclude(b => b.MainCat)
                .FirstOrDefault(ap => ap.Id == productId);
            /*  public int Quantity { get; set; }
          public string Name { get; set; }
          public int TotalPrice { get; set; }
          public int TotalGrossPrice { get; set; }
          public int ProductId { get; set; } // na itt van az hogy melyik termék tartozik hozzá
          public Packaging Pack { get; set; }*/

            ShoppingCart shoppingCart = new ShoppingCart
            {
                Product = product
            }; // létrehozunk egy új foglalást, amelynek megadjuk az terméket


            return shoppingCart;
        }

    }
}
