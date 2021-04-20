using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class SubCatViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public int MainCatId { get; set; }
        public static explicit operator SubCat(SubCatViewModel vm) => new SubCat
        {
            Id = vm.Id,
            Name = vm.Name,
        };
        public static explicit operator SubCatViewModel(SubCat i) => new SubCatViewModel
        {
            Id = i.Id,
            Name = i.Name
        };
    }
}
