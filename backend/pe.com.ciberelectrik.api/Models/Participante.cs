using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("participante")]
    public class Participante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idparticipante")]
        public long IdParticipante { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        [Column("boleto_id")]
        public long? BoletoId { get; set; }
    }
}