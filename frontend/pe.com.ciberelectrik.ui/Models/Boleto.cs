using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.ui.Models
{
    [Table("boleto")]
    public class Boleto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public long IdBoleto { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string CodigoBoleto { get; set; }

        [Display(Name = "Fecha de Apartado")]
        public DateTime? FechaApartado { get; set; }

        [Display(Name = "Fecha de Compra")]
        public DateTime? FechaCompra { get; set; }

        [Display(Name = "Comprado")]
        public long Comprado { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
