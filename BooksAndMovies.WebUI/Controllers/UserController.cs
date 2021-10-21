using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models.UserAccount;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public object Session { get; private set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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
                return View(user);
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            _userService.Add(entity: user);
            
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {

            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return View(model: user);
                }
            }
            var account = _userService.GetAll(x => x.Email == user.Email).SingleOrDefault();
            if (account != null)
            {
                bool passwordVerified = BCrypt.Net.BCrypt.Verify(user.Password, account.Password);
                if (passwordVerified)
                {
                    HttpContext.Session.SetString(key: "email", value: account.Email);
                    HttpContext.Session.SetString(key: "firstname", value: account.FirstName);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorMessage"] = "Login failed!";
            return View(model: user);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
