using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using TESTPROJECT.Models.ViewModels;

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
            var allCategories = _context.Categories.ToList();

            var model = new ProductViewModel { Categories = allCategories , Products = allProducts};
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(string ProductName, int ProductPrice, string ProductDescription, int CategoryId)
        {
            var product = new Product();
            //

            product.Name = ProductName;
            product.Price = ProductPrice;
            product.Description = ProductDescription;
            product.CategoryId = CategoryId;

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
