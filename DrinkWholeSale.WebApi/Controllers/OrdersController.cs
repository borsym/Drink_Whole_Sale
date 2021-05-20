using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.Shopping;
using DrinkWholeSale.Persistence.DTO;
using DrinkWholeSale.Persistence.Services;
using Microsoft.AspNetCore.Authorization;

namespace DrinkWholeSale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;

        public OrdersController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            return _service.GetOrders().Select(list => (OrderDto)list).ToList();

        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<ShoppingCartDto>> GetOrder(int id)
        {
            var order = (OrderDto)_service.GetOrder(id);
            var shippingCart = order.Items;
            if (order == null)
            {
                return NotFound();
            }

            return shippingCart;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutOrderAsync(int id, OrderDto order)
        {

            if (id != order.Id)
            {
                return BadRequest();
            }
            bool resutlt = await _service.SetStateOrderAsync((Order)order);
            if (resutlt)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
