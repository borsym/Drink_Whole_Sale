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

namespace DrinkWholeSale.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCatsController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;
      
        public MainCatsController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: api/MainCats
        [HttpGet]
        public ActionResult<IEnumerable<MainCatDto>> GetMainCats()
        {
            return _service.GetMainCats().Select(m => (MainCatDto)m).ToList();
        }

        // GET: api/MainCats/5
        [HttpGet("{id}")]
        public ActionResult<MainCatDto> GetMainCat(int id)
        {
            try
            {
                return (MainCatDto) _service.GetMainCatById(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // PUT: api/MainCats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutMainCat(int id, MainCatDto mainCat)
        {
            if (id != mainCat.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateMainCat((MainCat)mainCat))
                return Ok();
          

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // POST: api/MainCats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<MainCat> PostMainCat(MainCatDto mainCat)
        {
            var maincat = _service.CreateMainCat((MainCat)mainCat);
            if(maincat == null) return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction("GetMainCat", new { id = maincat.Id }, (MainCatDto)maincat);
        }

        // DELETE: api/MainCats/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMainCat(int id)
        {
            if (_service.DeleteMainCat(id))
                return Ok();


            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        
    }
}
