using DrinkWholeSale.Persistence.Shopping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Persistence.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int GuestId { get; set; }
        public bool fulfilled { get; set; }

        public static explicit operator Order(OrderDto dto) => new Order
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            Phone = dto.Phone,
            Email = dto.Email,
            GuestId = dto.GuestId,
            fulfilled = dto.fulfilled
        };

        public static explicit operator OrderDto(Order m) => new OrderDto
        {
            Id = m.Id,
            Name = m.Name,
            Address = m.Address,
            Phone = m.Phone,
            Email = m.Email,
            GuestId = m.GuestId,
            fulfilled = m.fulfilled
        };
    }
}
