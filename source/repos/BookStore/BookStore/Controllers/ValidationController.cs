using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data.Models;

namespace BookStore.Controllers
{
    public class ValidationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        private bool IsAlpha(string s)
        {
            foreach(char c in s)
            {
                bool check = ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c ==' ' || c==':');
                if (!check) return false;
            }
            return true;
        }
        private bool IsUpperCase(string s)
        {
            if (!(s[0] >= 'A' && s[0] <= 'Z')) return false;
            for(int i = 0; i < s.Length -1; i++)
            {
                if (s[i] == ' ' && !(s[i+1] >= 'A' && s[i+1] <= 'Z')) return false;
            }
            return true;
        }
       
        public IActionResult CheckTitle(string Title)
        {
            if(Title==null)
                return Json(data: "Title is required");
            if (!IsAlpha(Title))
                return Json(data: "Title must consist of letters 'a-z' or 'A-Z' only");
            if(!IsUpperCase(Title))
                return Json(data: "Each word must begin with an upper case letter" );
            return Json(data: true);
        }
        public IActionResult CheckAuthor(string Author)
        {
            if (Author == null)
                return Json(data: "Author is required");
            if (!IsAlpha(Author))
                return Json(data: "Author must consist of letters 'a-z' or 'A-Z' only");
            if (!IsUpperCase(Author))
                return Json(data: "Each word must begin with an upper case letter");
            return Json(data: true);
        }
        public IActionResult CheckDescription(string Description)
        {
            string[] words = Description.Split();
            string[] badWords = { "shit", "damn", "rubbish" };
            foreach(string word in words)
            {
                foreach(string bad in badWords)
                {
                    if(word.ToLower() == bad)
                    {
                        return Json(data: $"Don't you know that {word} is a bad word?");
                    }
                }
            }
            return Json(data: true);
        }


        private bool UperLower(string password)
        {
            bool u = false, l = false;
            foreach(char c in password)
            {
                if (c >= 'a' && c <= 'z') l = true;
                if (c >= 'A' && c <= 'Z') u = true;
            }
            return u & l;
        }
        private bool HasNumber(string Password)
        {
            foreach (char c in Password)
            {
                if (c >= '0' && c <= '9') return true;
            }
            return false;
        }


        public IActionResult CheckPassword(string Password)
        {
            if(Password.Length < 8)
                return Json(data: "Password lengs must be at least 8");
            if(!UperLower(Password))
                return Json(data: "Password must contain at least one uppercase and one lowercase letter");
            if (!HasNumber(Password))
                return Json(data: "Password must contain at least one number");
            return Json(data: true);
        }


        private bool FreeUserName(string Name)
        {
            User user = null;
            using (BookContext context = new BookContext())
            {
                user = context.Users.FirstOrDefault(u => u.Name == Name);
            }
            return (user == null);
        }

        public IActionResult CheckUsername(string Name)
        {
            if(!FreeUserName(Name))
            {
                List<string> sufix = new List<string>();
                for(int i = 1; i <= 47; i++)
                {
                    sufix.Add(Convert.ToString(i));
                }
                string suggestion = "";
                foreach(string s in sufix)
                {
                    suggestion = Name + s;
                    if(FreeUserName(suggestion))
                    {
                        break;
                    }
                }
                return Json(data: $"This name is already taken.\n Suggestion for you: {suggestion}");
            }
            else
            {
                return Json(data: true);
            }
        }
    }
}