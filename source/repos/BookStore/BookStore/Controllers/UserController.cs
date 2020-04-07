using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Models;
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
            ViewBag.massage = s;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public string Register(User user)
        {
            user.Password = Crypto.HashPassword(user.Password);
            user.PasswordConfirm = user.Password;
            db.Users.Add(user);
            db.SaveChanges();
            return "Welcome!";
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
        public string Login(User model)
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
                    return "Wrong password";
                }
                Response.Cookies.Append("Id", Convert.ToString(user.Id));
                Response.Cookies.Append("Role", user.Role);
                return "Welcome ";
            }
            else
            {
                return "No user with this username and password\n";
            }
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
            return View();
        }
        [HttpPost]
        public string PostBook(Book book)
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
            return res;
        }

        [HttpGet]
        public ActionResult MyBooks()
        {
            int CurUserId = Convert.ToInt32(Request.Cookies["Id"]);
            IEnumerable<Book> books = db.Books.Where(u => u.SellerId == CurUserId); ;
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Request.Cookies["Id"] == null)
                return LocalRedirect("~/User/Login");
          
            var book = db.Books.FirstOrDefault(u => u.Id == Id);
            
            if(book == null)
            {
                return LocalRedirect("~/User/MyBooks");
            }
            else
            {
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
        [HttpPost]
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
    }


}
