using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<Guest> _signInManager;

        public AccountController(SignInManager<Guest> signInManager) 
        {
            _signInManager = signInManager;
        }
        //api/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if(result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized("Login faild!");
        }
        //api/Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
