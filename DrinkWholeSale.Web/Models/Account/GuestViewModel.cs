using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Persistence
{
    public class GuestViewModel
    {
        /// <summary>
        /// Vendég neve.
        /// </summary>
        [Required(ErrorMessage = "The name is required.")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "The booker's name maximum 60 character.")]
        public String GuestName { get; set; }

        /// <summary>
        /// Vendég e-mail címe.
        /// </summary>
        [Required(ErrorMessage = "The e-mail is required")]
        [EmailAddress(ErrorMessage = "The e-mail format is not valid")]
        [DataType(DataType.EmailAddress)] // pontosítjuk az adatok típusát
        public String GuestEmail { get; set; }

        /// <summary>
        /// Vendég címe.
        /// </summary>
        [Required(ErrorMessage = "The address is required")]
        public String GuestAddress { get; set; }

        /// <summary>
        /// Vendég telefonszáma.
        /// </summary>
        [Required(ErrorMessage = "The phone number is required")]
        [Phone(ErrorMessage = "The phone number format is not valid")]
        [RegularExpression(@"06[237]0([0-9]{7})", ErrorMessage = "The phone number format is not valid")]
        [DataType(DataType.PhoneNumber)]
        public String GuestPhoneNumber { get; set; }
    }
}
