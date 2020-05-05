using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;


namespace BookStore.Controllers
{
    
    public class UserController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Message(string s)
        {
            ViewBag.message = s;
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            user.PasswordConfirm = user.Password;
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Message", new { s = "Welcome!" });

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            User user = null;

            using (BookContext context = new BookContext())
            {
                string username = model.Name;
                user = context.Users.FirstOrDefault(u => u.Name == username );
              
            }
            if (user != null)
            {
                if(!Crypto.VerifyHashedPassword(user.Password, model.Password))
                {
                    return RedirectToAction("Message", new { s = "Wrong password" });
                }
                if(user.Blocked)
                {
                    return RedirectToAction("Message", new { s = "Your account is blocked" });
                }
                Response.Cookies.Append("Id", Convert.ToString(user.Id));
                Response.Cookies.Append("Role", user.Role);
                return RedirectToAction("Message", new { s = "Welcome!" });
            }
            else
            {
   
                return RedirectToAction("Message", new { s = "No user with this username and password" });
            }
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Id");
            Response.Cookies.Delete("Role");
            return RedirectToAction("Message", new { s = "Success" });
        }

        public string GetInfo()
        {
            
            string res;
            string CurUserId = Request.Cookies["Id"];
            if(CurUserId == null)
            {
                res = "You are not logged in";
            }
            else
            {
                string curId = Convert.ToString(CurUserId);
                res = "Current user id is " + curId;
            }
            return res;
        }

        public IActionResult Profile(int Id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == Id);
            if(user == null)
            {
                return RedirectToAction("Message", new { s = "Not found" });
            }
            ViewBag.user = user;
            ViewBag.books = null;
            if(user.Role == "Seller")
            {
                ViewBag.books = db.Books.Where(u => u.SellerId == Id);
            }
            ViewBag.allow = false;
            if (Request.Cookies["Id"] != null)
            {
                int curId = Convert.ToInt32(Request.Cookies["Id"]);
                if (Id == curId) ViewBag.allow = true;
            }
          
            return View();
        }

        public IActionResult Me()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            return RedirectToAction("Profile", new { Id = Id });
        }      

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            int curId = Convert.ToInt32(Request.Cookies["Id"]);
            if(Id != curId)
            {
                return RedirectToAction("Message", new { s = "Not allowed" });
            }
            User user = db.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User form)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == form.Id);
            user.Name = form.Name;
            user.Age = form.Age;
            user.Email = form.Email;
            db.SaveChanges();
            return RedirectToAction("Profile", new { Id = user.Id });
        }
    }


}
