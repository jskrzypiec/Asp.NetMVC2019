using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ComicBooks.Controllers
{
    public class CustomLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}