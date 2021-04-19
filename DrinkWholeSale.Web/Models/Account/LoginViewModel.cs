using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.Web.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// Felhasználónév.
        /// </summary>
        [Required(ErrorMessage = "A felhasználónév megadása kötelező.")]
        public String UserName { get; set; }

        /// <summary>
        /// Jelszó.
        /// </summary>
        [Required(ErrorMessage = "A jelszó megadása kötelező.")]
        [DataType(DataType.Password)]
        public String UserPassword { get; set; }

        public bool RememberLogin { get; set; }
    }
}
