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
using System.Threading.Tasks;


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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _UserManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var viewModel = new AdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(AdminViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _UserManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            user.UserName = model.UserName;
            user.Email = model.UserEmail;

            var result = await _UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTotal(string id)
        {
            var user = await _UserManager.FindByIdAsync(id);

            if (user != null)
            {
                await _UserManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }


    }
}

