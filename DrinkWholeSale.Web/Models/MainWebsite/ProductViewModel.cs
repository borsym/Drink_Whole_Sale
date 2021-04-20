using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "A nev megadása kötelező"), MaxLength(30, ErrorMessage = "maximum 30 karakter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Köteleő megadni a gyártót"), MaxLength(30)]
        public string Producer { get; set; }
        [Required(ErrorMessage = "Köteleő megadni számot")]
        public int TypeNumber { get; set; }
        [Required(ErrorMessage = "Köteleő megadni a nettot")]
        public int NetPrice { get; set; }
        [Required(ErrorMessage = "Köteleő megadni a bruttót")]
        public int GrossPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Packaging Pack { get; set; }
       
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int SubCatId { get; set; }  // foreign key

        public static explicit operator Product(ProductViewModel vm) => new Product
        {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Producer = vm.Producer,
            Image = vm.Image,
            SubCatId = vm.SubCatId,
            NetPrice = vm.NetPrice,
            GrossPrice = vm.GrossPrice,
            Quantity = vm.Quantity,
            Pack = vm.Pack

        };
        public static explicit operator ProductViewModel(Product i) => new ProductViewModel
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Producer = i.Producer,
            Image = i.Image,
            SubCatId = i.SubCatId,
            NetPrice = i.NetPrice,
            GrossPrice = i.GrossPrice,
            Quantity = i.Quantity,
            Pack = i.Pack
        };
    }
}
