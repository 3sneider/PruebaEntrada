using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NexosTest.Entities.Entities
{
    public class Paciente
    {
        public int id { get; set; }

        [Required]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "el campo numeroSeguro debe tener {1} caracteres")]
        public string numeroSeguro { get; set; }

        [Required]
        public string telefonoContacto { get; set; }
                
        public string codigoPostal { get; set; }

        public Doctor doctor { get; set; }
    }
}
