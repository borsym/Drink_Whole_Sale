using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Persistence.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public int TypeNumber { get; set; }
        public int NetPrice { get; set; }
        public int Quantity { get; set; }
        public Packaging Pack { get; set; }
        public int GrossPrice { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int SubCatId { get; set; }

        public static explicit operator Product(ProductDto dto) => new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Producer = dto.Producer,
            TypeNumber = dto.TypeNumber,
            NetPrice = dto.NetPrice,
            Quantity = dto.Quantity,
            Pack = dto.Pack,
            GrossPrice = dto.GrossPrice,
            Description = dto.Description,
            Image = dto.Image,
            SubCatId = dto.SubCatId
        };

        public static explicit operator ProductDto(Product m) => new ProductDto
        {
            Id = m.Id,
            Name = m.Name,
            Producer = m.Producer,
            TypeNumber = m.TypeNumber,
            NetPrice = m.NetPrice,
            Quantity = m.Quantity,
            Pack = m.Pack,
            GrossPrice = m.GrossPrice,
            Description = m.Description,
            Image = m.Image,
            SubCatId = m.SubCatId
        };

    }
}
