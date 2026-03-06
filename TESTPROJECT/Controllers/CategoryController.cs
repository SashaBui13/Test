using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using TESTPROJECT.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TESTPROJECT.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var allCategories = _context.Categories.ToList();
            return View(allCategories);
        }

        [HttpPost]
        public IActionResult AddCategory(string CategoryName, string CategoryDescription, IFormFile ImageFile)
        {
            string imageUrl = null;

            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
                imageUrl = "/images/categories/" + uniqueFileName;
            }

            var category = new Category
            {
                Name = CategoryName,
                Description = CategoryDescription,
                ImageUrl = imageUrl
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult InCategory(int id)
        {
            var product = _context.Products
                .Include(pr => pr.Category)
                .Where(pr => pr.CategoryId == id)
                .ToList();

            if (product == null) return NotFound();

            var viewModel = new ProductViewModel
            {
                Products = product,
            };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryViewModel
            {
                categoryId = category.Id,
                categoryName = category.Name,
                categoryDescription = category.Description,
                categoryImageUrl = category.ImageUrl,
                Categories = _context.Categories.Where(c => !c.IsDeleted).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model, IFormFile ImageFile)
        {
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.ToList();
                return View(model);
            }

            var category = _context.Categories.Find(model.categoryId);
            if (category == null) return NotFound();

            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "categories");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
                category.ImageUrl = "/images/categories/" + uniqueFileName;
            }

            category.Name = model.categoryName;
            category.Description = model.categoryDescription;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return View("Error");

            category.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}