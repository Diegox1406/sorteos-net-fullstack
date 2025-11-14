using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.ui.Models
{
    [Table("participante")]
    public class Participante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public long IdParticipante { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Fecha de Actualización")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Usuario ID")]
        public long? UserId { get; set; }

        [Display(Name = "Boleto ID")]
        public long? BoletoId { get; set; }
    }
}