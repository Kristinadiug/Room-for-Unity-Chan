using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Buyer: Users
    {
        List<Purchase> Purchases { set; get; }
    }
}
