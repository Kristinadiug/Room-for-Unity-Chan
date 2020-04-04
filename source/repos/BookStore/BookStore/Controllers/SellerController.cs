using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Models;


namespace BookStore.Controllers
{
    public class SellerController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private BookContext _context = new BookContext();
        public SellerController( IWebHostEnvironment appEnvironment)
        {

            _appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PostBook()
        {
            return View();
        }
        [HttpPost]
        public string Create(Book book)
        {
            string res = "Book was added ";
            if(true)
            {
                byte[] imageData = null;
            
                using (var binaryReader = new BinaryReader(book.ImageData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)book.ImageData.Length);
                }
                book.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageData);
                res += "with image";
            }
            book.SellerId = 3;
          
            _context.Books.Add(book);
            _context.SaveChanges();
            return res;
        }
    }
}