using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.DTO;
using DrinkWholeSale.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkWholeSale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;

        public ShoppingCartController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

    }
}
