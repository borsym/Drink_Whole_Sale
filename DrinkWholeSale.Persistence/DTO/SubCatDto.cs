using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkWholeSale.Persistence.DTO
{
    public class SubCatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCatId { get; set; }

        public static explicit operator SubCat(SubCatDto dto) => new SubCat
        {
            Id = dto.Id,
            Name = dto.Name,
            MainCatId = dto.MainCatId
        };

        public static explicit operator SubCatDto(SubCat m) => new SubCatDto
        {
            Id = m.Id,
            Name = m.Name,
            MainCatId = m.MainCatId
        };

    }
}
