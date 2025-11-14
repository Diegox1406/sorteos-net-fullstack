using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("tipodocumento")]
    public class TipoDocumento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Codigo")]
        [Column("codtipd")]
        public int codigo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("nomtipd")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [Column("esttipd")]
        public bool estado { get; set; }
    }
}