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

        public async Task<IActionResult> Details(int Id)
        {
            Book book = db.Books.FirstOrDefault(u => u.Id == Id);
            IEnumerable<Review> reviews = db.Reviews.Where(u => u.BookId == Id);
            User seller = db.Users.FirstOrDefault(u => u.Id == book.SellerId);

            reviews.ToList();
            reviews.Reverse();
            
            double sum = 0;
            foreach(var r in reviews)
            {
                sum += r.Rating;
            }

            ViewBag.book = book;
            ViewBag.reviews = reviews;
            ViewBag.seller = seller.Name;
            ViewBag.rate = 0;
            ViewBag.role = Request.Cookies["Role"];
            if (reviews.Count() != 0) ViewBag.rate = Math.Round(sum / reviews.Count(), 2);

            return View();
        }
        [HttpPost]
        [Route("Book/PostReview")]
        public IActionResult PostReview(ReviewForm form, int bookId)
        {
            Review review = new Review();
            int id = Convert.ToInt32(Request.Cookies["Id"]);
            review.UserId = id;
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            review.UserName = user.Name;
            review.Comment = form.Comment;
            review.Rating = form.Rating;
            review.BookId = bookId;

            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", new { Id = bookId });
        }
    }
}