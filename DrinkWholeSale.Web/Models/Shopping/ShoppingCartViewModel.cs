using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{
    public class ShoppingCartViewModel : GuestViewModel
    {
        public Product Product { get; set; } //itt nem productnak hanem a bevásárlólistának kéne lennie

        [DataType(DataType.Currency)]
        public Int32 TotalPrice { get; set; }
        [DataType(DataType.Currency)]
        public Int32 TotalGrossPrice { get; set; }
    }
}
