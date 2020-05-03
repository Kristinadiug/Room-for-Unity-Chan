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
        
        public ActionResult Users()
        {
            ViewBag.Users = db.Users;
            return View();
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
                ViewBag.Role = "1";
                return RedirectToAction("Message", new { s = "Welcome!" });
            }
            else
            {
   
                return RedirectToAction("Message", new { s = "No user with this username and password" });
            }
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Id"); ;
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

        [HttpGet]
        public ActionResult PostBook()
        {
            if(Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            User user = db.Users.FirstOrDefault(u => u.Id == Id);
            if(user == null || user.Role != "Seller")
            {
                return RedirectToAction("Message", new { s = "You are not a seller" });
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult PostBook(Book book)
        {
            string res = "Book was added ";

            if (book.ImageData != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(book.ImageData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)book.ImageData.Length);
                }
                book.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageData);
                res += "with image";
            }
            string CurUserId = Request.Cookies["Id"];
            book.SellerId = Convert.ToInt32(CurUserId);

            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Message", new { s = "Book was added" });
        }

        [HttpGet]
        public ActionResult MyBooks()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            User user = db.Users.FirstOrDefault(u => u.Id == Id);
            if (user == null || user.Role != "Seller")
            {
                return RedirectToAction("Message", new { s = "You are not a seller" });
            }
            IEnumerable<Book> books = db.Books.Where(u => u.SellerId == Id); ;
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int userId = Convert.ToInt32(Request.Cookies["Id"]);
            User user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null || user.Role != "Seller")
            {
                return RedirectToAction("Message", new { s = "You are not a seller" });
            }
            if (Request.Cookies["Id"] == null)
                return LocalRedirect("~/User/Login");
          
            var book = db.Books.FirstOrDefault(u => u.Id == Id);
            
            if(book == null)
            {
                return RedirectToAction("Message", new { s = "Not found" });
            }
            else
            {
                if(book.SellerId != userId)
                {
                    return RedirectToAction("Message", new { s = "You have no rights for this book" });
                }
                ViewBag.Model = book;
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            int Id = book.Id;
            Book oldData = db.Books.FirstOrDefault(u => u.Id == Id);
            book.SellerId = oldData.SellerId;
            book.ImageUrl = oldData.ImageUrl;
            if (book.ImageData != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(book.ImageData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)book.ImageData.Length);
                }
                book.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageData);
            }
            db.Entry(oldData).State = System.Data.Entity.EntityState.Deleted;
            db.Entry(book).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("MyBooks");
                     
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
       
        public IActionResult DeleteSure(int Id)
        {
            Book book = null;
            book = db.Books.FirstOrDefault(u => u.Id == Id);
            if(book == null)
            {
                return NotFound();
            }
            else
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("MyBooks");
            }
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
                user.Blocked = true;
                db.SaveChanges();
                return RedirectToAction("Message", new { s = "User was blocked" });
            }
        }
    }


}
