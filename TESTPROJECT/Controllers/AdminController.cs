using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;


namespace TESTPROJECT.Controllers
{
    public class AdminController : Controller
    {
        public UserManager<IdentityUser> _UserManager { get; set; }
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = UserManager;
            _roleManager = roleManager;
        }
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;



        // GET: AdminController
        public ActionResult Index()
        {
            var users = _UserManager.Users.ToList();
            var role = _roleManager.Roles.ToList();
            return View(users);
        }

        /*public IActionResult Edit(int id)
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
        */



    }
}

