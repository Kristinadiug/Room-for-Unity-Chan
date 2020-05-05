using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        BookContext db = new BookContext();
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Users()
        {
            ViewBag.Users = db.Users;
            return View();
        }


        [HttpGet]
        public IActionResult Block(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public IActionResult BlockSure(int Id)
        {
            User user = null;
            user = db.Users.FirstOrDefault(u => u.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Blocked = !user.Blocked;
                db.SaveChanges();
                if (user.Blocked) return RedirectToAction("Message", "Home", new { s = "User was blocked" });
                else return RedirectToAction("Message", "Home", new { s = "User was unblocked" });
            }
        }
    }
}