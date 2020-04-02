using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        public ushort Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Availability{ get; set; }
        public int Popularity{ get; set; }
    }
}
