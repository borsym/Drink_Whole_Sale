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
    public class MainCatsController : ControllerBase
    {
        private readonly IDrinkWholeSaleService _service;

        public MainCatsController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: api/MainCats
        [HttpGet]
        public ActionResult<IEnumerable<MainCat>> GetMainCats()
        {
            return _service.GetMainCats();
        }

        //// GET: api/MainCats/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MainCat>> GetMainCat(int id)
        //{
        //    var mainCat = await _context.MainCats.FindAsync(id);

        //    if (mainCat == null)
        //    {
        //        return NotFound();
        //    }

        //    return mainCat;
        //}

        //// PUT: api/MainCats/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMainCat(int id, MainCat mainCat)
        //{
        //    if (id != mainCat.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mainCat).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MainCatExists(id))
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

        //// POST: api/MainCats
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<MainCat>> PostMainCat(MainCat mainCat)
        //{
        //    _context.MainCats.Add(mainCat);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMainCat", new { id = mainCat.Id }, mainCat);
        //}

        //// DELETE: api/MainCats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<MainCat>> DeleteMainCat(int id)
        //{
        //    var mainCat = await _context.MainCats.FindAsync(id);
        //    if (mainCat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MainCats.Remove(mainCat);
        //    await _context.SaveChangesAsync();

        //    return mainCat;
        //}

        //private bool MainCatExists(int id)
        //{
        //    return _context.MainCats.Any(e => e.Id == id);
        //}
    }
}
