using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{
   
    public class DbInitializer
    {
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
        public static void Initialize(DrinkWholeSaleDbContext context, string imageDirectory)
        {
            context.Database.Migrate();
            if (context.MainCats.Any()) return;

            var orangePath = Path.Combine(imageDirectory, "narancsle.png");
            var waterPath = Path.Combine(imageDirectory, "viz.png");
            var vodkaPath = Path.Combine(imageDirectory, "vodka.png");
            var whiskyPath = Path.Combine(imageDirectory, "whisky.png");
            var szen_vizPath = Path.Combine(imageDirectory, "szen_viz.png");
            var royal_vodkaPath = Path.Combine(imageDirectory, "royal_vodka.png");
            var narancs_sioPath = Path.Combine(imageDirectory, "narancs_sio.png");
            var jim_bimPath = Path.Combine(imageDirectory, "jim_bim.png");

            
            IList<MainCat> defaultLists = new List<MainCat>()
            {
                new MainCat
                {
                    Name = "Alcoholic drinks",
                    SubCats = new List<SubCat>
                    {
                        new SubCat
                        {
                            Name = "Vodka",
                            Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Royal Vodka",
                                    Producer = "Pias Kft",
                                    TypeNumber = 1,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "This will take you to the floor",
                                    Quantity = 5,
                                    Pack = getPacking(5),
                                    Image = File.Exists(royal_vodkaPath) ? File.ReadAllBytes(royal_vodkaPath) : null
                                },
                                 new Product
                                {
                                    Name = "Absolut Vodka",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 2,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Very good product",
                                    Image = File.Exists(vodkaPath) ? File.ReadAllBytes(vodkaPath) : null,
                                    Quantity = 90,
                                    Pack = getPacking(90)
                                },
                            }
                        },
                        new SubCat
                        {
                            Name = "Whisky",
                            Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "very Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth tastem",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image = File.Exists(jim_bimPath) ? File.ReadAllBytes(jim_bimPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Smooth taste",
                                    Image = File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =  File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                                  new Product
                                {
                                    Name = "Jim Whisky",
                                    Producer = "Pias Kft",
                                    TypeNumber = 3,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Smooth taste",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 2000,
                                    Pack = getPacking(2000)
                                },
                                 new Product
                                {
                                    Name = "Whisky Jack",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 4,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image =   File.Exists(whiskyPath) ? File.ReadAllBytes(whiskyPath) : null,
                                    Quantity = 13,
                                    Pack = getPacking(13)
                                },
                            }
                        }

                    }
                },
                new MainCat
                {
                    Name = "Alcohol free drinks"
,
                    SubCats = new List<SubCat>
                    {
                        new SubCat
                        {
                            Name = "Viz",
                            Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Sparkling water",
                                    Producer = "Pias Kft",
                                    TypeNumber = 5,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "ocean taste",
                                    Image = File.Exists(szen_vizPath) ? File.ReadAllBytes(szen_vizPath) : null,
                                    Quantity = 6,
                                    Pack = getPacking(6)
                                },
                                 new Product
                                {
                                    Name = "Non-carbonated water",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 6,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "better",
                                    Image = File.Exists(waterPath) ? File.ReadAllBytes(waterPath) : null,
                                    Quantity = 99999,
                                    Pack = getPacking(99999)
                                },
                            }
                        },
                        new SubCat
                        {
                            Name = "Orange Juice",
                            Products = new List<Product>
                            {
                                new Product
                                {
                                    Name = "Very good orange2.0",
                                    Producer = "Pias Kft",
                                    TypeNumber = 7,
                                    NetPrice = 1000,
                                    GrossPrice = 1270,
                                    Description = "Juice Juice Juice",
                                    Image = File.Exists(narancs_sioPath) ? File.ReadAllBytes(narancs_sioPath) : null,
                                    Quantity = 1456,
                                    Pack = getPacking(1456)
                                },
                                 new Product
                                {
                                    Name = "Very good orange",
                                    Producer = "Nagy Pias Kft",
                                    TypeNumber = 8,
                                    NetPrice = 1500,
                                    GrossPrice = 1905,
                                    Description = "Adom adom",
                                    Image = File.Exists(orangePath) ? File.ReadAllBytes(orangePath) : null,
                                    Quantity = 5012,
                                    Pack = getPacking(5012)
                                },
                            }
                        }

                    }

                }
            };

            context.AddRange(defaultLists);
            
            context.SaveChanges();
        }
    }
}
