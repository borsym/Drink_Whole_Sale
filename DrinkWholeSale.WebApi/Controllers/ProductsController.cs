using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.Services;
using DrinkWholeSale.Persistence.DTO;
using Microsoft.AspNetCore.Authorization;

namespace DrinkWholeSale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;

        public ProductsController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: api/Products/Subcat    ez lehet hibas lesz
        [HttpGet("SubCat/{subcatId}")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts(int subcatId)
        {
            try
            {
                return _service.GetSubCatById(subcatId)
                    .Products.Select(s => (ProductDto)s)
                    .ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        //GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            try
            {
                var result = (ProductDto)_service.GetProductById(id);
                return result;
            }
            catch (NullReferenceException)
            {

                return NotFound();
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateItem((Product)product))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<Product> PostProduct(ProductDto product)
        {
            var item = _service.CreateProduct((Product)product);
            if (item is null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, (ProductDto)item);
        }

        // DELETE: api/Products/5
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (_service.DeleteProduct(id))
                return Ok();
            
            return StatusCode(StatusCodes.Status500InternalServerError);
            
        }
    }
}
