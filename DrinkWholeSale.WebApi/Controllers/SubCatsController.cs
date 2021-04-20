using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.Services;

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

        // GET: api/SubCats/6
        [HttpGet("{maincatId}")]
        public ActionResult<IEnumerable<SubCat>> GetSubCats(int maincatId)
        {
            try
            {
                return _service.GetMainCatById(maincatId)
                    .SubCats
                    .ToList();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        //// GET: api/SubCats/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SubCat>> GetSubCat(int id)
        //{
        //    var subCat = await _context.SubCats.FindAsync(id);

        //    if (subCat == null)
        //    {
        //        return NotFound();
        //    }

        //    return subCat;
        //}

        //// PUT: api/SubCats/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSubCat(int id, SubCat subCat)
        //{
        //    if (id != subCat.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(subCat).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SubCatExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/SubCats
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<SubCat>> PostSubCat(SubCat subCat)
        //{
        //    _context.SubCats.Add(subCat);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSubCat", new { id = subCat.Id }, subCat);
        //}

        //// DELETE: api/SubCats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<SubCat>> DeleteSubCat(int id)
        //{
        //    var subCat = await _context.SubCats.FindAsync(id);
        //    if (subCat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SubCats.Remove(subCat);
        //    await _context.SaveChangesAsync();

        //    return subCat;
        //}

        //private bool SubCatExists(int id)
        //{
        //    return _context.SubCats.Any(e => e.Id == id);
        //}
    }
}
