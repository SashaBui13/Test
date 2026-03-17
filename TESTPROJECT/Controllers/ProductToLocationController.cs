using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using TESTPROJECT.Models.ViewModels;


//using TESTPROJECT.Migrations;
using YourProjectName.Controllers;

namespace TESTPROJECT.Controllers
{
    

    public class ProductToLocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductToLocationController(ApplicationDbContext context)
        {
                _context = context;
        }
        public IActionResult Index(int locationId)
        {
            var ptl = _context.ProductsToLocations.Include(pl => pl.Product).Include(pl => pl.Location).Where(pl=>pl.LocationId == locationId).ToList();
            if(ptl == null) return NotFound();
            var viewmodel = new ProductToLocationViewModel
            {
                productToLocations = ptl,
            

            };
                return View(viewmodel);
        }

        public IActionResult Edit(int id)
        {

            var ptl = _context.ProductsToLocations.Include(pl => pl.Product).Include(pl => pl.Location).Where(pl => pl.Id == id).FirstOrDefault();
            if (ptl == null) return NotFound();
            var viewmodel = new ProductToLocationViewModel
            {
               ProductToLocation = ptl

            };
            return View(viewmodel);
        }
        [HttpPost]
        public IActionResult Edit(ProductToLocationViewModel model)
        {
            var ptl = _context.ProductsToLocations.Find(model.Id);
            if(ptl == null) return NotFound();
            ptl.Quantity = model.Quantity;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AutoStartFill()
        {
            var product = _context.Products.ToList();
            var location = _context.Locations.ToList();
            
            foreach(var loc in location)
            {
                
                foreach(var prod in product)
                {
                    var ptl = new ProductToLocation();
                    var loh = _context.ProductsToLocations.Where(x => x.ProductId == prod.Id && x.LocationId == loc.LocationId).ToList();
                    if(ptl.Quantity == 0) ptl.IsDeleted = true;
                    if (loh.Count() > 0) continue;
                    ptl.ProductId = prod.Id;
                    ptl.LocationId = loc.LocationId;
                    _context.ProductsToLocations.Update(ptl);
                    _context.SaveChanges();
                }

            }
            var productToLocation = new ProductToLocation
            {
                
            };
            return RedirectToAction("Index", "Home");
        }

        

    }
}
