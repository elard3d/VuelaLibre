using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public IActionResult Index(Account account)
        {
            ViewBag.Accounts = _context.Accounts.ToList();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Register() // GET
        {
            return View("Register", new Account());
        }

        [HttpPost]
        public ActionResult Register(Account account, string contraseña, string correo, string verfcontraseña) // POST
        {
            var correos = _context.Accounts.ToList();
            foreach (var item in correos)
            {
                if (item.Correo == correo)
                    ModelState.AddModelError("Correo","El correo ya existe, ingrese otro correo");
            }
            
            if (ModelState.IsValid)
            {
                account.Contraseña = CreateHash(contraseña);
                account.VerfContraseña = CreateHash(verfcontraseña);
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View("Register", account);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account account, string Correo, string Contraseña)
        {
            var user = _context.Accounts.Where(o => o.Correo == Correo && o.Contraseña == CreateHash(Contraseña))
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

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CrearVuelo() // GET
        {
            ViewBag.Destinos = _context.ListVuelo.ToList();
            ViewBag.Aerolineas = _context.Aerolineas.ToList();
            ViewBag.fechas = DateTime.Now;
            ViewBag.fecha = DateTime.Now;
            return View("CrearVuelo", new Vuelo());
        }

        [HttpPost]
        public ActionResult CrearVuelo(Vuelo vuel, DateTime fechaida, DateTime fecharegreso) // POST
        {
            var now = DateTime.Now;
            int fechaResultado = DateTime.Compare(fechaida, now);
            if (fechaResultado < 0)
                ModelState.AddModelError("FechaIda", "La fecha de salida debe ser mayor a la actual");

            var ahora = DateTime.Now;
            int fechaResul = DateTime.Compare(fecharegreso, ahora);
            if (fechaResul < 0)
                ModelState.AddModelError("FechaRegreso", "La fecha de regreso debe ser mayor a la actual");

            if (ModelState.IsValid)
            {
                _context.Vuelos.Add(vuel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Destinos = _context.ListVuelo.ToList();
            ViewBag.Aerolineas = _context.Aerolineas.ToList();
            return View("CrearVuelo");
        }

        [HttpGet]
        public ActionResult MostrarVuelos()
        {
            var vuelo =  _context.Vuelos.ToList();
           // ViewBag.vuelos = _context.Vuelos.ToList();
            return View(vuelo);
        }
        [HttpPost]
        public ActionResult Buscar(Vuelo vuelo, string origen,string destino, DateTime fechaida, DateTime fecharegreso)
        {
            
            var mostrar = _context.Vuelos.Where(o => o.Origen == origen && o.Destino == destino && o.FechaIda >= fechaida && o.FechaRegreso <= fecharegreso)
                .ToList();
            if (mostrar != null)
            {
                var now = DateTime.Now;
                int fechaResultado = DateTime.Compare(fechaida, now);
                if (fechaResultado < 0)
                    ModelState.AddModelError("FechaIda", "La fecha de salida debe ser mayor a la actual");

                var nowfecha = DateTime.Now;
                int fechaResultados = DateTime.Compare(fecharegreso, nowfecha);
                if (fechaResultados < 0)
                    ModelState.AddModelError("FechaRegreso", "La fecha de regreso debe ser mayor a la actual");
                return View(mostrar);
            }
            return View("Index", vuelo);
        }
        private string CreateHash(string input)
        {
            var sha = SHA256.Create();
            input += configuration.GetValue<string>("Token");
            var hash = sha.ComputeHash(Encoding.Default.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        [Authorize]
        public ActionResult Comprar(Vuelo vuelo, int id)
        {

            var mostrar = _context.Vuelos.Where(o => o.Id == id).ToList();
            if (mostrar != null)
            {
                return View(mostrar);
            }
            return View(vuelo);
        }

        public ActionResult Perfil ()
        {

            
            return View();
        }


    }
}
