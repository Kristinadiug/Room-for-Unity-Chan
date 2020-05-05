using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data.Models;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BuyerController : Controller
    {
        BookContext db = new BookContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Purchases()
        {
            if (Request.Cookies["Id"] == null)
                return LocalRedirect("~/User/Login");
            int Id = Convert.ToInt32(Request.Cookies["Id"]);
            List<PurchaseView> purchaseView = new List<PurchaseView>();
            IEnumerable<Purchase> purchases = db.Purchases.Where(u => u.BuyerId == Id);
            foreach (var p in purchases)
            {
                Book book = db.Books.FirstOrDefault(u => u.Id == p.ProductId);
                if (book == null) continue;
                User seller = db.Users.FirstOrDefault(u => u.Id == book.SellerId);
                PurchaseView pv = new PurchaseView { BookId = book.Id, SellerId = seller.Id, SellerName = seller.Name, BookName = book.Title, Date = p.Date, Price = book.Price, Id = p.Id };
                purchaseView.Add(pv);
            }
            ViewBag.purchases = purchaseView;
            return View();
        }

        public IActionResult Buy(int Id)
        {
            if (Request.Cookies["Id"] == null)
                return LocalRedirect("~/User/Login");
            int UserId = Convert.ToInt32(Request.Cookies["Id"]);
            DateTime curDate = DateTime.Now;

            Purchase p = new Purchase { BuyerId = UserId, ProductId = Id, Date = curDate };
            db.Purchases.Add(p);
            db.SaveChanges();
            return LocalRedirect("~/Buyer/Purchases");
        }
    }
}