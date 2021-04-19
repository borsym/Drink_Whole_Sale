using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Web.Models;
using System.Diagnostics;

namespace DrinkWholeSale.Web.Controllers
{
    public class SubCatsController : Controller
    {
        private readonly DrinkWholeSaleDbContext _context;

        public SubCatsController(DrinkWholeSaleDbContext context)
        {
            _context = context;
        }

        // GET: SubCats
        public IActionResult Index(int id)
        {
            Debug.WriteLine("ez az id:" + id);
            var drinkWholeSaleDbContext = _context.MainCats.Include(s => s.SubCats).Single(l => l.Id == id).SubCats.ToList();/*GetSubCatsByMainCatId(id) seq cont no element*/;
            return View(drinkWholeSaleDbContext);
        }

        // GET: SubCats/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCat =  _context.SubCats
                .Include(s => s.MainCat)
                .FirstOrDefault(m => m.Id == id);
            if (subCat == null)
            {
                return NotFound();
            }

            return View(subCat);
        }

        // GET: SubCats/Create
        public IActionResult Create()
        {
            ViewData["MainCatId"] = new SelectList(_context.MainCats, "Id", "Name");
            return View();
        }

        // POST: SubCats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,MainCatId")] SubCatViewModel vm)
        {
            var subCat = (SubCat)vm;
            if (ModelState.IsValid)
            {
               _context.Add(subCat);
               _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainCatId"] = new SelectList(_context.MainCats, "Id", "Name", subCat.MainCatId);
            return View(subCat);
        }

        // GET: SubCats/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCat =  _context.SubCats.Find(id);
            if (subCat == null)
            {
                return NotFound();
            }
            ViewData["MainCatId"] = new SelectList(_context.MainCats, "Id", "Name", subCat.MainCatId);
            return View(subCat);
        }

        // POST: SubCats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Name,MainCatId")] SubCatViewModel vm)
        {
            var subCat = (SubCat)vm;
            if (id != subCat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCat);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCatExists(subCat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainCatId"] = new SelectList(_context.MainCats, "Id", "Name", subCat.MainCatId);
            return View(subCat);
        }

        // GET: SubCats/Delete/5
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCat =  _context.SubCats
                .Include(s => s.MainCat)
                .FirstOrDefault(m => m.Id == id);
            if (subCat == null)
            {
                return NotFound();
            }

            return View(subCat);
        }

        // POST: SubCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var subCat = _context.SubCats.Find(id);
            _context.SubCats.Remove(subCat);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCatExists(int id)
        {
            return _context.SubCats.Any(e => e.Id == id);
        }
    }
}
