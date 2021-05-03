using DrinkWholeSale.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence.Services
{
    public interface IDrinkWholeSaleService
    {
        Task<bool> AddOrder(List<ShoppingCart> list, string userName);
        List<MainCat> GetMainCats(String name = null);
        MainCat GetMainCatById(int? id);
        List<SubCat> GetProductByMainCatId(int id);
        List<SubCat> GetSubCats(String name = null);
        SubCat GetSubCatById(int id);
        List<Product> GetProductBySubCatId(int id);
        MainCat GetMainCatDetails(int? id);
        MainCat EditMainCat(int? id);
        bool RemoveMainCat(MainCat mainCat);
        bool IsMainCatExists(int? id);
        bool AddMainCat(MainCat mainCat);
        List<SubCat> GetSubCatsByMainCatId(int id);
        bool UpdateMainCat(MainCat mainCat);
        IEnumerable<MainCat> MainCats { get; }
        IEnumerable<SubCat> SubCats { get; }
       // IEnumerable<ShoppingCart> ShoppingCarts { get; }
       
      
        ShoppingCart newShoppingCartAdd2(int? productId);



        public Product GetProductById(int? id);
        bool DeleteMainCat(int id);
        MainCat CreateMainCat(MainCat mainCat);
        bool UpdateSubCat(SubCat subCat);
        SubCat CreateSubCat(SubCat subCatdto);
        bool DeleteSubCat(int id);
        bool UpdateItem(Product product);
        Product CreateProduct(Product product);
        bool DeleteProduct(int id);

        //  void AddItemShoppingCart(ShoppingCart cartItem);
        //  void RemoveItemShoppingCart(ShoppingCart cartItem);
        // ShoppingCart GetShoppingCartProduct(int id);
        //ShoppingCart GetShoppingCartById(int id);

    }
}
