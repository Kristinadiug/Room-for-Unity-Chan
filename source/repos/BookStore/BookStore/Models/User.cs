using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class Users
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Age { set; get; }
        public String Password { set; get; }
    }
}
