using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class TVShowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
