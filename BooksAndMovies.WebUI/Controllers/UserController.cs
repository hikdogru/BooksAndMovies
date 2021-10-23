using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BooksAndMovies.WebUI.Controllers
{
    
    public class UserController : Controller
    {
        #region fields

        private readonly IUserService _userService;

        #endregion fields

        #region ctor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion ctor

        #region methods
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
            var account = GetAccountByEmail(email: user.Email);
            if (account != null)
            {
                bool passwordVerified =IsPasswordVerified(user.Password, account.Password);
                if (passwordVerified)
                {
                    CreateUserSession(user: account);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorMessage"] = "Login failed!";
            return View(model: user);
        }

        [HttpPost]
        public IActionResult LoginWithDemoAccount()
        {
            var appsettingsDemoUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("DemoUser");
            string email = appsettingsDemoUser["Email"];
            string password = appsettingsDemoUser["Password"];
            var account = GetAccountByEmail(email:email);
            if (account != null)
            {
                bool passwordVerified = IsPasswordVerified(password, account.Password);
                if (passwordVerified)
                {
                    CreateUserSession(user: account);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["ErrorMessage"] = "Login failed!";
            return View("Login");
        }

        private void CreateUserSession(User user)
        {
            HttpContext.Session.SetString(key: "email", value: user.Email);
            HttpContext.Session.SetString(key: "firstname", value: user.FirstName);
        }

        private bool IsPasswordVerified(string enteredPassword, string accountPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, accountPassword);
        }

        private User GetAccountByEmail(string email)
        {
            return _userService.GetAll(x => x.Email == email).SingleOrDefault();
        }
        


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        #endregion methods

    }
}
