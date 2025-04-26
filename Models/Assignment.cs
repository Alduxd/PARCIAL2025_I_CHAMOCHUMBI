using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FutbolPeruano.Models
{
    [Index(nameof(PlayerId), nameof(TeamId), IsUnique = true, Name = "IX_Assignment_Player_Team_Unique")]
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string Notes { get; set; }

        // Navegación
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
