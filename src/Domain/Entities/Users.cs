using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcologAPI.src.Domain.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        [Required]
        [StringLength(255)]
        public string? Email { get; set; }
        
        [Required]
        [StringLength(50)]
        public string? Password { get; set; }

        [Required]
        [StringLength(10)]
        public string? Profile { get; set; }
    }
}