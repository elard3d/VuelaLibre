using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VuelaLibre.Models;
using VuelaLibre.Models.Maps;

namespace VuelaLibre.Controllers
{
    public class AccountController : Controller
    {
        private readonly VuelaLibreContext _context;
        private readonly IConfiguration configuration;

        public AccountController(VuelaLibreContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public string Indexx(string password)
        {
            return CreateHash(password);
        }
        [HttpGet]
        public ActionResult Register() // GET
        {
            return View("Register", new Account());
        }

        [HttpPost]
        public ActionResult Register(Account account) // POST
        {

            if (ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Register", account);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Correo, string Contraseña)
        {
            var user = _context.Accounts.Where(o => o.Correo == Correo && o.Contraseña == Contraseña)
                .FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Correo)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Account");
            }
            ModelState.AddModelError("Login", "Usuario o contraseña incorrectos.");
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        [HttpGet]
        public ActionResult CrearVuelo() // GET
        {
            return View(new Vuelos());
        }

        [HttpPost]
        public ActionResult CrearVuelo(Vuelos vuelo) // POST
        {

            if (ModelState.IsValid)
            {
                _context.Vuelo.Add(vuelo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vuelo);
        }
    }
}
