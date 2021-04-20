using DrinkWholeSale.Persistence.Services;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int TotalPrice { get; set; }
        public int TotalGrossPrice { get; set; }
        
        public Packaging Pack { get; set; }

        [Required]
        public int ProductId { get; set; }// na itt van az hogy melyik termék tartozik hozzá
        public virtual Product Product { get; set; }
        //[ForeignKey("Guest")]
        //public string UserId { get; set; }  //melyik felhasználóhoz tartozik a bevásárlókocsi

    }
}
