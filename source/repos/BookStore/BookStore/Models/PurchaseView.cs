using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class PurchaseView
    {
        public int Id { set; get; }
        public string BookName { set; get; }
        public string SellerName { set; get; }
        public int BookId { set; get; }
        public int SellerId { set; get; }
        public int Price { set; get; }
        public DateTime Date { set; get; }
    }
}
