using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VuelaLibre.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        [Required]
        public string Origen { get; set; }
        [Required]
        public string Destino { get; set; }
        [Required]
        public string Aerolinea { get; set; }
        [Required]
        public DateTime FechaIda { get; set; }
        [Required]
        public DateTime FechaRegreso { get; set; }
        [Required]
        public int TiempoVuelo { get; set; }
        [Required]
        public int TotalPasaje { get; set; }
        [Required]
        public decimal Precio { get; set; }
    }
}
