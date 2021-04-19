using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{
    public class AddShoppingCartViewModel
    {
        public Product Product { get; set; }
        [Required(ErrorMessage = "Köteleő megadni a mennyiséget")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Köteleő megadni kiszerelést")]
        public Packaging Pack { get; set; }
        

        [DataType(DataType.Currency)]
        public Int32 TotalPrice { get; set; }
        [DataType(DataType.Currency)]
        public Int32 TotalGrossPrice { get; set; }
    }
}
