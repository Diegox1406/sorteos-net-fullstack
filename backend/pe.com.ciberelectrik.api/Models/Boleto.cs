using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("boleto")]
    public class Boleto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idboleto")]
        public long IdBoleto { get; set; }

        [Required]
        [StringLength(7)]
        [Column("boleto")]
        public string CodigoBoleto { get; set; }

        [Column("fechaApartado")]
        public DateTime? FechaApartado { get; set; }

        [Column("fechaCompra")]
        public DateTime? FechaCompra { get; set; }

        [Required]
        [Column("comprado")]
        public long Comprado { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}