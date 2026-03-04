using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TESTPROJECT.Data;
using TESTPROJECT.Models;

namespace YourProjectName.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allProducts = _context.Products.ToList();
            return View(allProducts);
        }

        [HttpPost]
        public IActionResult AddProduct(string ProductName, int ProductPrice, string ProductDescription)
        {
            var product = new Product();


            product.Name = ProductName;
            product.Price = ProductPrice;
            product.Description = ProductDescription;


            _context.Products.Add(product);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return View("Error");
            }
            product.IsDeleted= true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
