using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data.Models
{
    public class Book: Products
    {
        [Required]
        [Remote(action: "CheckTitle", controller: "Validation")]
        public string Title { get; set; }
        [Required]
        [Remote("CheckAuthor", "Validation")]
        public string Author { get; set; }
        public string[] Genres { get; set; }

    }
}
