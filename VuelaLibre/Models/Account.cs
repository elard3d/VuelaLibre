using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Nombre { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression(@"[a-zA-Z]+")]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [MinLength(6)]
        public string Contraseña { get; set; }
        [Required]
        [MinLength(6)]
        public string VerfContraseña { get; set; }
    }
}
