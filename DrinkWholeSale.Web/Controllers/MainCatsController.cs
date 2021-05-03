using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Persistence;
using DrinkWholeSale.Persistence.Services;

using PagedList.Mvc;
using PagedList;
namespace DrinkWholeSale.Web.Controllers
{
    public class MainCatsController : Controller
    {
        //private readonly DrinkWholeSaleDbContext _context;
        private readonly IDrinkWholeSaleService _service;

        public MainCatsController(IDrinkWholeSaleService service)
        {
            _service = service;
        }

        // GET: MainCats
        public IActionResult Index(int? i)
        {
            //const int PageSize = 3; // you can always do something more elegant to set this

            //var count = this.dataSource.Count();

            //var data = this.dataSource.Skip(page * PageSize).Take(PageSize).ToList();

            //this.ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            //this.ViewBag.Page = page;
            int pageNumber = (i ?? 1);
            return View(_service.GetMainCats().ToPagedList(pageNumber, 20));  // indexbe is atirtam
            
        }

        // GET: MainCats/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCat = _service.GetMainCatDetails(id);
            if (mainCat == null)
            {
                return NotFound();
            }

            return View(mainCat);
        }

        // GET: MainCats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainCats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] MainCat mainCat)
        {
            if (ModelState.IsValid)
            {
                var result =  _service.CreateMainCat(mainCat);
                if(result != null)
                    return NotFound();
                
                return RedirectToAction(nameof(Index));
            }
            return View(mainCat);
        }

        // GET: MainCats/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCat =  _service.EditMainCat(id);
            if (mainCat == null)
            {
                return NotFound();
            }
            return View(mainCat);
        }

        // POST: MainCats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] MainCat mainCat)
        {
            if (id != mainCat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(mainCat);
                    //_context.SaveChanges();
                    bool result = _service.UpdateMainCat(mainCat);
                    if(result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error saving");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainCatExists(mainCat.Id))
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
            return View(mainCat);
        }

        // GET: MainCats/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainCat = _service.GetMainCatById(id);


            if (mainCat == null)
            {
                return NotFound();
            }

            return View(mainCat);
        }

        // POST: MainCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var mainCat = _service.EditMainCat(id);
            _service.RemoveMainCat(mainCat);
            return RedirectToAction(nameof(Index));
        }

        private bool MainCatExists(int id)
        {
            return _service.IsMainCatExists(id);
        }
    }
}
