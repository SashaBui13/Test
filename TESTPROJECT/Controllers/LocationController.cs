using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;
using Microsoft.AspNetCore.Hosting;
using TESTPROJECT.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace TESTPROJECT.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocationController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var locations = _context.Locations.ToList();
            var model = new LocationViewModel
            {
                Location = locations
            };

            return View(model);
        }

        public IActionResult RemovedLocations()
        {
            var locations = _context.Locations.ToList();
            var model = new LocationViewModel
            {
                Location = locations
            };

            return View(model);
        }

        public IActionResult Add()
        {
            var locations = _context.Locations.ToList();
            var model = new LocationViewModel
            {
                Location = locations
            };

            return View(model);
        }
        public IActionResult AddLocation(string locationName, string locationAdress, string locationMapsPath)
        {
            var location = new Location
            {
                LocationName = locationName,
                LocationAdress = locationAdress,
                LocationMapsPath = locationMapsPath,
                
            };

            _context.Locations.Add(location);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

      public IActionResult Edit(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return NotFound();

            var viewModel = new LocationViewModel
            {
                LocationName = location.LocationName,
                LocationAdress = location.LocationAdress,
                LocationMapsPath = location.LocationMapsPath,
                LocationId = location.LocationId,
                Location = _context.Locations.Where(l => !l.LocationIsDeleted).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(LocationViewModel model)
        {
            var location = _context.Locations.Find(model.LocationId);
            if (location == null) return NotFound();
            location.LocationName = model.LocationName;
            location.LocationAdress = model.LocationAdress;
            location.LocationMapsPath = model.LocationMapsPath;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return View("Error");

            location.LocationIsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTotal(int id)
        {
            var location = _context.Locations.Find(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToLocations(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return View("Error");

            location.LocationIsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
