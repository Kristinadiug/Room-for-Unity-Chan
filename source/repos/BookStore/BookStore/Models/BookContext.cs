using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Shop.Data.Models;

namespace BookStore.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { set; get; }
        public DbSet<Purchase> Purchases { set; get; }
        public DbSet<Users> Users { set; get; }
        public DbSet<Seller> Sellers { set; get; }
        public DbSet<Buyer> Buyers { set; get; }
    }

    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Title = "Война и мир", Author = "Л. Толстой", Price = 220 });
            db.Books.Add(new Book { Title = "Отцы и дети", Author = "И. Тургенев", Price = 180 });
            db.Books.Add(new Book { Title = "Чайка", Author = "А. Чехов", Price = 150 });

            base.Seed(db);
        }
    }
}
