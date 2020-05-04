using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
        [DefaultValue(3)]
        public int pageSize { get; set; }
    }

}
