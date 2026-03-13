using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;

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
        public IActionResult Index()
        {
            return View();
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
                    ptl.ProductId = prod.Id;
                    ptl.LocationId = loc.LocationId;
                    _context.ProductsToLocations.Update(ptl);
                    _context.SaveChanges();
                }

            }
            var productToLocation = new ProductToLocation
            {
                
            };
            return View();
        }
    }
}
