using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class SellerController : Controller
    {
        BookContext db = new BookContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PostBook()
        {
            if (Request.Cookies["Id"] == null)
            {
                return LocalRedirect("~/User/Login");
            }
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            User user = db.Users.FirstOrDefault(u => u.Id == Id);
            if (user == null || user.Role != "Seller")
            {
                return RedirectToRoute("default", new { Controller = "Home", Action = "Message", s = "You are not a seller" });
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
            return RedirectToAction("Message", "Home", new { s = "Book was added" });
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
                return RedirectToRoute("default", new { Controller = "Home", Action = "Message", s = "You are not a seller" });
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
                return RedirectToRoute("default", new {Controller="Home", Action="Message", s = "You are not a seller" });
            }
            if (Request.Cookies["Id"] == null)
                return LocalRedirect("~/User/Login");

            var book = db.Books.FirstOrDefault(u => u.Id == Id);

            if (book == null)
            {
                return RedirectToRoute("default", new { Controller = "Home", Action = "Message", s = "Not Found" });
            }
            else
            {
                if (book.SellerId != userId)
                {
                    return RedirectToRoute("default", new { Controller = "Home", Action = "Message", s = "You have no rights for this book" });
                    
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
            book.Id = oldData.Id;
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
            if (book == null)
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