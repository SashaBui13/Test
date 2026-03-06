using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using TESTPROJECT.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace YourProjectName.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            var model = new ProductViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                LongDescription = product.LongDescription,
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        public IActionResult RemovedProducts()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();

            var model = new ProductViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(model);
        }

        public IActionResult Product()
        {
            var allProducts = _context.Products.ToList();
            var allCategories = _context.Categories.ToList();

            var model = new ProductViewModel { Categories = allCategories, Products = allProducts };
            return View(model);
        }

       // [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(string ProductName, int ProductPrice, string ProductDescription, int CategoryId, IFormFile ImageFile, string LongDescription)
        {
            string imageUrl = null;

            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
                imageUrl = "/images/products/" + uniqueFileName;
            }

            var product = new Product
            {
                Name = ProductName,
                Price = ProductPrice,
                Description = ProductDescription,
                CategoryId = CategoryId,
                ImageUrl = imageUrl,
                LongDescription = LongDescription
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

      // [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                LongDescription = product.LongDescription,
                Categories = _context.Categories.Where(c => !c.IsDeleted).ToList()
            };

            return View(viewModel);
        }

      //  [HttpPost]
       // [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductViewModel model, IFormFile ImageFile)
        {


            ModelState.Remove("ImageUrl");
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.ToList();
                return View(model);
            }

            var product = _context.Products.Find(model.Id);
            if (product == null) return NotFound();

            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");

                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
                product.ImageUrl = "/images/products/" + uniqueFileName;
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.LongDescription = model.LongDescription;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

       // [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return View("Error");

            product.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTotal(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToSell(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return View("Error");

            product.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}