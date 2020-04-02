using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Seller: Users
    {
        public List<Products> Products { set; get; }
        public List<Purchase> Sales { set; get; }
    }
}
