using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VuelaLibre.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Aerolinea { get; set; }
        public DateTime FechaIda {get; set;}
        public TimeSpan HoraSalida { get; set; }
        public DateTime FechaRegreso { get; set; }
        public TimeSpan HoraRegreso { get; set; }
        public int TiempoVuelo { get; set; }
        public int TotalPasaje { get; set; }
        public decimal Precio { get; set; }

    }
}
