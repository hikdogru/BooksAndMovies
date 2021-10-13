using BooksAndMovies.Entity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class UserController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState.ToList();
                return View(user);
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);


            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
