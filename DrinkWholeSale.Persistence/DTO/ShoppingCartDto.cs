using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Persistence.DTO
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int TotalPrice { get; set; }
        public int TotalGrossPrice { get; set; }
        public Packaging Pack { get; set; }
        public int ProductId { get; set; }


        public static explicit operator ShoppingCart(ShoppingCartDto dto) => new ShoppingCart
        {
            Id = dto.Id,
            Name = dto.Name,
            Quantity = dto.Quantity,
            TotalGrossPrice = dto.TotalGrossPrice,
            Pack = dto.Pack,
            TotalPrice = dto.TotalPrice,
            ProductId = dto.ProductId
        };

        public static explicit operator ShoppingCartDto(ShoppingCart m) => new ShoppingCartDto
        {
            Id = m.Id,
            Name = m.Name,
            Quantity = m.Quantity,
            TotalGrossPrice = m.TotalGrossPrice,
            Pack = m.Pack,
            TotalPrice = m.TotalPrice,
            ProductId = m.ProductId
        };
    }
}
