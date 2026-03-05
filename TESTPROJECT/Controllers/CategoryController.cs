using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using TESTPROJECT.Models.ViewModels;

namespace TESTPROJECT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        public ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
                _context = context;
        }
        // GET: CategoryController
        public IActionResult Index()
        {
            var allCategories = _context.Categories.ToList();
            return View(allCategories);
        }
        [HttpPost]
        public IActionResult AddCategory(string CategoryName, string CategoryDescription)
        {
            var category = new Category();

            category.Name = CategoryName;
            category.Description = CategoryDescription;

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            var viewModel = new CategoryViewModel
            {
                categoryId = category.Id,
                categoryName = category.Name,
                categoryDescription = category.Description,
                Categories = _context.Categories
                                     .Where(c => !c.IsDeleted)
                                     .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.ToList();
                return View(model);
            }
                var category = _context.Categories.Find(model.categoryId);
                category.Name = model.categoryName;
                category.Description = model.categoryDescription;

                _context.SaveChanges();

                return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return View("Error");
            }
            category.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
