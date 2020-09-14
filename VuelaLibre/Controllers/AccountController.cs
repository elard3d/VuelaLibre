using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VuelaLibre.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }

        public ViewResult Register()
        {
            return View("Register");
        }

    }
}
