using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { set; get; }
        public int UserId { set; get; }
        public int BookId { set; get; }
        public string UserName { set; get; }
        public int Rating { set; get; }
        public string Comment { set; get; }
    }
    public class ReviewForm
    {
        public int Rating { set; get; }
        public string Comment { set; get; }
        public int BookId { set; get; }
    }
}
