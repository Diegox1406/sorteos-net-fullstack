using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("distrito")]
    public class Distrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        [Column("coddist")]
        public int codigo { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("nomdist")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [Column("estdist")]
        public bool estado { get; set; }
    }
}