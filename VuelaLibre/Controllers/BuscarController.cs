using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VuelaLibre.Models;
using VuelaLibre.Models.Maps;

namespace VuelaLibre.Controllers
{
    public class BuscarController : Controller
    {
        private readonly VuelaLibreContext _context;

        public BuscarController(VuelaLibreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult MostrarVuelos()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MostrarVuelos(Vuelos vuelo, string origen, string destino)
        {
            var mostrar = _context.Vuelo.Where(o => o.Origen == origen && o.Destino == destino)
                .FirstOrDefault();
            if (mostrar != null)
            {
                var accounts = _context.Accounts.ToList();
                return View(vuelo);
            }
            return View();
        }
    }
}