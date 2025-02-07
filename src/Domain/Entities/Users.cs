using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [StringLength(200)]
        public string? Email { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [Required]
        [StringLength(10)]
        public string? Profile { get; set; }

        public Users(int id, string name, string email, byte[] passwordHash, byte[] passwordSalt )
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Users(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void AlterarSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

    }
}