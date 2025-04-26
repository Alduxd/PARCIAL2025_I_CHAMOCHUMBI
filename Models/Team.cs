using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutbolPeruano.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "La ciudad no puede exceder los 100 caracteres")]
        public string City { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FoundedDate { get; set; }

        // Navegación
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
