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
    public class SubCatsController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;

        public SubCatsController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: api/SubCats/MainCat/6
        [HttpGet("MainCat/{maincatId}")]
        public ActionResult<IEnumerable<SubCatDto>> GetSubCats(int maincatId)
        {
            try
            {
                return _service.GetMainCatById(maincatId)
                    .SubCats.Select(s => (SubCatDto)s)
                    .ToList();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        // GET: api/SubCats/5
        [HttpGet("{id}")]
        public ActionResult<SubCatDto> GetSubCat(int id)
        {
            try
            {
                return (SubCatDto)_service.GetSubCatById(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        // PUT: api/SubCats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPut("{id}")]
        public IActionResult PutSubCat(int id, SubCatDto subCat)
        {
            if (id != subCat.Id)
            {
                return BadRequest();
            }

            

            if (_service.UpdateSubCat((SubCat)subCat))
                return Ok();
            
            
            return StatusCode(StatusCodes.Status500InternalServerError);
            
        }

        // POST: api/SubCats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "administrator")]
        [HttpPost]
        public ActionResult<SubCat> PostSubCat(SubCatDto subCatdto)
        {
            var item = _service.CreateSubCat((SubCat)subCatdto);
            if(item is null)
               return  StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetSubCat), new { id = item.Id }, (SubCatDto)item);
        }

        // DELETE: api/SubCats/5
        [Authorize(Roles = "administrator")]
        [HttpDelete("{id}")]
        public ActionResult<SubCat> DeleteSubCat(int id)
        {
            if (_service.DeleteSubCat(id))
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
