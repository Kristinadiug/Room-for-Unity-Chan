using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore.Data.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        [Required]
        [Range(0,10000000, ErrorMessage ="Price must be non-negative")]
        public int Price { get; set; }

        [Required]
        [Remote("CheckDescription", "Validation")]
        public string Description { get; set; }
        public int Availability{ get; set; }
        public int Popularity{ get; set; }
    }
}
