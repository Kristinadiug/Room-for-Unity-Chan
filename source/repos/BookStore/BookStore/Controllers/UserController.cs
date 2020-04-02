using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BookStore.Controllers
{
    
    public class UserController : Controller
    {
        BookContext db = new BookContext();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public string RegisterSeller(Seller user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            db.Sellers.Add(user);
            db.SaveChanges();
            return "Welcome Seller!";
        }
        [HttpPost]
        public string RegisterBuyer(Buyer user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            db.Buyers.Add(user);
            db.SaveChanges();
            return "Welcome Buyer!";
        }
        public ActionResult Users()
        {
            IEnumerable<Users> users = db.Users;
            ViewBag.Buyers = db.Buyers;
            ViewBag.Sellers = db.Sellers;
            return View();
        }
    }
}
