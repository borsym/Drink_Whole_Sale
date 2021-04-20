using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class SubCat
    {
        public SubCat()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        
        [Required]
        public int MainCatId { get; set; }
        public virtual MainCat MainCat { get; set; }
    }
}
