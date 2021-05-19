using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DrinkWholeSale.Persistence;
using Microsoft.AspNetCore.Identity;
using DrinkWholeSale.Persistence.Shopping;

namespace DrinkWholeSale.Persistence
{
    public class DrinkWholeSaleDbContext : IdentityDbContext<Guest, IdentityRole<int>,int>
    {
            public DbSet<MainCat> MainCats { get; set; }
            public DbSet<SubCat> SubCats { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Order> Orders { get; set; }
             public DbSet<Guest> Guests { get; set; }
        // public DbSet<CartItem> ShoppingCartItems { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Guest>().ToTable("Guests");
            // A felhasználói tábla alapértelmezett neve AspNetUsers lenne az adatbázisban, de ezt felüldefiniálhatjuk.
        }
        public DrinkWholeSaleDbContext(DbContextOptions<DrinkWholeSaleDbContext> options) : base(options) { } //<> most került bele 0401

        
      

    }
}
