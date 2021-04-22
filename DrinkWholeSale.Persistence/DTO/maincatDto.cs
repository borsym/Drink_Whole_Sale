using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Persistence.DTO
{
    public class MainCatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator MainCat(MainCatDto dto) => new MainCat
        {
            Id = dto.Id,
            Name = dto.Name
        };

        public static explicit operator MainCatDto(MainCat m) => new MainCatDto
        {
            Id = m.Id,
            Name = m.Name
        };

    }
}
