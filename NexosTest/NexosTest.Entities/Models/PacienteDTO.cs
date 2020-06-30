using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NexosTest.Entities.Models
{
    public class PacienteDTO
    {
        public int id { get; set; }

        [Required]
        public string nombreCompleto { get; set; }

        [Required]
        public string numeroSeguro { get; set; }

        [Required]
        public string telefonoContacto { get; set; }
    }
}
