using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NexosTest.Entities.Entities
{
    public class Doctor
    {
        public int id { get; set; }

        [Required]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "el campo numeroCredencial debe tener {1} caracteres")]
        public string numeroCredencial { get; set; }

        [Required]
        public string especialidad { get; set; }

        [Required]
        public string hospitalResidente { get; set; }

        public List<Paciente> Pacientes { get; set; }
    }
}
