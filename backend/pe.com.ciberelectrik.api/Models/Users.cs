using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pe.com.ciberelectrik.api.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [StringLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Required, StringLength(255)]
        [Column("email")]
        public string Email { get; set; }

        [Required, StringLength(255)]
        [Column("username")]
        public string Username { get; set; }

        [Required, StringLength(255)]
        [Column("password")]
        public string Password { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}