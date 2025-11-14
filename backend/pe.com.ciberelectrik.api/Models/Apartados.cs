using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("apartados")]
    public class Apartados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idapartados")]
        public long IdApartados { get; set; }

        [Required]
        [Column("boleto")]
        public int Boleto { get; set; }

        [Required]
        [Column("fechaapartados")]
        public DateTime? FechaApartados { get; set; }

        [Column("boleto_id")]
        public long? BoletoId { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
