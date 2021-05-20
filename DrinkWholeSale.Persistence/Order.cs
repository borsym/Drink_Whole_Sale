using DrinkWholeSale.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence.Shopping
{
    public class Order
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual List<ShoppingCart> items { get; set; }  
        public virtual  Guest Guest { get; set; }
        public int GuestId { get; set; }
        public bool fulfilled { get; set; }
        public DateTime orderDate { get; set; }
    }
}
