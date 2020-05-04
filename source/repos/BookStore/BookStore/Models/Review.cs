using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { set; get; }
        public int UserId { set; get; }
        public int BookId { set; get; }
        public string UserName { set; get; }
        [Required]
        public int Rating { set; get; }
        [Required]
        [Remote("CheckDescription", "Validation")]
        public string Comment { set; get; }
    }
}
