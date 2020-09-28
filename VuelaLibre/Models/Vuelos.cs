using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VuelaLibre.Models.Maps
{
    public class Vuelos
    {
        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Aerolinea { get; set; }
        public DateTime Fecha_Hora {get; set;}
        public int TiempoVuelo { get; set; }
        public int TotalPasaje { get; set; }
        public decimal Precio { get; set; }

    }
}
