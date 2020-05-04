using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        BookContext db = new BookContext();
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Details(int Id)
        {
            Book book = db.Books.FirstOrDefault(u => u.Id == Id);
            IEnumerable<Review> reviews = db.Reviews.Where(u => u.BookId == Id);
            reviews.ToList();
            
            double sum = 0;
            foreach(var r in reviews)
            {
                sum += r.Rating;
            }

            ViewBag.book = book;
            ViewBag.reviews = reviews;
            ViewBag.rate = 0;
            if (reviews.Count() != 0) ViewBag.rate = sum / reviews.Count();

            return View();
        }
    }
}