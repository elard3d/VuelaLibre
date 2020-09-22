using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VuelaLibre.Models;

namespace VuelaLibre.Controllers
{
    public class AccountController : Controller
    {
        private VuelaLibreContext _context;
        public AccountController(VuelaLibreContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }


        [HttpGet]
        public ActionResult Register() // GET
        {
            return View("Register", new Account());
        }

        [HttpPost]
        public ActionResult Register(Account account) // POST
        {
            //if(account.Name == null || account.Name == "")
            //    ModelState.AddModelError("Name1", "El campo Nombre es requerido");

            if (ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Register", account);
        }

        public ViewResult Login()
        {
            return View("Login");
        }

        

    }
}
