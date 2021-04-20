using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrinkWholeSale.Persistence;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DrinkWholeSale.Web.Controllers
{
    public enum sortOrder { PRODUCER_DESC, PRODUCER_ASC, NETPRICE_DESC, NETPRICE_ASC }
    public class ProductsController : Controller
    {
        public static Packaging getPacking(int quant)
        {
            if (quant >= 6 && quant < 12)
                return Packaging.SHRINK_FILM;
            if (quant >= 12 && quant < 24)
                return Packaging.SALVER;
            if (quant >= 24)
                return Packaging.TRAY;
            return Packaging.PIECE;
        }

        private readonly DrinkWholeSaleDbContext _context;

        public ProductsController(DrinkWholeSaleDbContext context)
        {
            _context = context;
        }
        public IActionResult DisplayImage(int id)
        {
            var item = _context.Products.FirstOrDefault(i => i.Id == id);
            if (item == null) return null;
            return File(item.Image, "image/png");
        }
        // GET: Products
        public IActionResult Index(int id, int page, sortOrder sortOrder = sortOrder.PRODUCER_ASC)
        {
            ViewData["NetPriceSortParam"] = sortOrder == sortOrder.NETPRICE_ASC ? sortOrder.NETPRICE_DESC : sortOrder.NETPRICE_ASC;
            ViewData["ProducerSortParam"] = sortOrder == sortOrder.PRODUCER_ASC ? sortOrder.PRODUCER_DESC : sortOrder.PRODUCER_ASC;
            //: 'Sequence contains no elements mind a két esetben de belefog kerülni
            var result = _context.SubCats.Include(i => i.Products).Single(i => i.Id == id);
            var totalPages = (int)Math.Ceiling((decimal)result.Products.Count() / 20); // hany oldal lesz
            ViewBag.TotalPages = totalPages;
            var tmp = result.Products.OrderByDescending(i => i.Id).Skip((page - 1) * 20).Take(20).ToList();
            var list = _context.SubCats.Include(p => p.Products).Single(i => i.Id == id);/*_context.SubCats.Include(p => p.Products).FirstOrDefault(i => i.Id ==id).Products.ToList();*/

            switch (sortOrder)
            {
                case sortOrder.PRODUCER_DESC:
                    tmp = tmp.OrderByDescending(i => i.Producer).ToList();  // dupla kattra nem rakja vissza rendesbe
                    break;
                case sortOrder.PRODUCER_ASC:
                    tmp = tmp.OrderBy(i => i.Producer).ToList();
                    break;
                case sortOrder.NETPRICE_DESC:
                    tmp = tmp.OrderByDescending(i => i.NetPrice).ToList();
                    break;
                case sortOrder.NETPRICE_ASC:
                    tmp = tmp.OrderBy(i => i.NetPrice).ToList();
                    break;
                default:
                    break;
            }
           // *1 -> 20

            return View(tmp/*list.Products.ToList()*/);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products
                .Include(p => p.SubCat)
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel vm, IFormFile image)
        {
            var product = (Product)vm;
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    product.Image = stream.ToArray();
                }
            }
            if (ModelState.IsValid)
            {
                product.Pack = getPacking(product.Quantity);
                _context.Add(product);
                _context.SaveChanges();
                 return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "SubCats", new { id = product.SubCatId });
            }
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "Id", "Name", product.SubCatId);
            return View(product);
        }

        // GET: Products/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "Id", "Name", product.SubCatId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Producer,TypeNumber,NetPrice,Quantity,Pack,GrossPrice,Description,Image,SubCatId")] ProductViewModel vm, IFormFile image)
        {

            if (id != vm.Id)
            {
                return NotFound();
            }
            var product = (Product)vm;
            if (image != null && image.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    image.CopyTo(stream);
                    product.Image = stream.ToArray();
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    if(product.Image == null)
                    {
                        _context.Entry(product).Property("Image").IsModified = false;
                    }
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["SubCatId"] = new SelectList(_context.SubCats, "Id", "Name", product.SubCatId);
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .Include(p => p.SubCat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product =  _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
