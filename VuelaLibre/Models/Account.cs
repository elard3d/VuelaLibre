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
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
