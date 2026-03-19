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
using Microsoft.CodeAnalysis;

namespace TESTPROJECT.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult AddComment(int ProductId, string UserName, string CommentText)
        {
            var comment = new Comment()
            {
                ProductId = ProductId,
                UserName = UserName,
                CommentText = CommentText
            };
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Home", new { id = ProductId });
        }
        public IActionResult DeleteComment(int id, int ProductId)
        {
            var comment = _context.Comments.Where(cm => cm.Id == id).FirstOrDefault();
            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Home", new { id = ProductId });
        }


    }
}
