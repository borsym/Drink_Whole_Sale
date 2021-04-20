using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class MainCat
    {
        public MainCat()
        {
            SubCats = new HashSet<SubCat>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<SubCat> SubCats { get; set; }
    }
}
