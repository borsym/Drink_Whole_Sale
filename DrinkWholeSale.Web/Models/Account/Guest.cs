using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{
    public class Guest : IdentityUser<int>
    {
        public Guest()
        {
            //ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
       
        
        //public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
