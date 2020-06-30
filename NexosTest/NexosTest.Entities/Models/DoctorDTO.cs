using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NexosTest.Entities.Models
{
    public class DoctorDTO
    {
        public int id { get; set; }

        [Required]
        public string nombreCompleto { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "el campo numeroCredencial debe tener {1} caracteres")]
        public string numeroCredencial { get; set; }

        [Required]
        public string especialidad { get; set; }
        
        public List<PacienteDTO> Pacientes { get; set; }
    }
}
