using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutbolPeruano.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria")]
        [Range(16, 45, ErrorMessage = "La edad debe estar entre 16 y 45 años")]
        public int Age { get; set; }

        [Required(ErrorMessage = "La posición es obligatoria")]
        [StringLength(50, ErrorMessage = "La posición no puede exceder los 50 caracteres")]
        public string Position { get; set; }

        // Navegación
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
