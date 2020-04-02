using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Book: Products
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string[] Genres { get; set; }

    }
}
