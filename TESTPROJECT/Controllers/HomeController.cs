using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient; 

namespace YourProjectName.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(string Name, decimal Price, string Description)
        {
            

            return RedirectToAction("Index");
        }
    }
}
