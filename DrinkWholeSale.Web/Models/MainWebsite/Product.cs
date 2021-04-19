using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{                          // db    folia       rekesz    tálca
    public enum Packaging { PIECE, SHRINK_FILM, SALVER, TRAY }
    
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, MaxLength(30)]
        public string Producer { get; set; }
        [Required]
        public int TypeNumber { get; set; }
        [Required]
        public int NetPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Packaging Pack { get; set; }
        [Required]
        public int GrossPrice { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public byte[] Image { get; set; }

        //[Required]
        //public int CartItemId { get; set; }  // foreign key
        //public virtual CartItem CartItem{ get; set; }
        [Required]
        public int SubCatId { get; set; }  // foreign key
        public virtual SubCat SubCat { get; set; }

        //public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
        
    }
}
