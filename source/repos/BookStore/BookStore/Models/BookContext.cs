using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using BookStore.Data.Models;

namespace BookStore.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { set; get; }
        public DbSet<Purchase> Purchases { set; get; }
        public DbSet<User> Users { set; get; }
    }

    
}
