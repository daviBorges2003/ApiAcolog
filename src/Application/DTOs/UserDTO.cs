using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Enums;

namespace Application.DTOs
{
    public class UserDTO
    {

        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é Obrigatório")]
        [MaxLength(250, ErrorMessage = "O nome deve ter no máximo 250 Caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é Obrigatório")]
        [MaxLength(200, ErrorMessage = "O e-mail deve ter no máximo 200 Caracteres")]
        public string? Email { get; set; }

        [NotMapped] 
        [Required(ErrorMessage = "A senha é um campo Obrigatório")]
        [MaxLength(200, ErrorMessage = "A senha deve ter no máximo 200 Caracteres")]
        [MinLength(8, ErrorMessage = "A senha deve ter  pelo menos 8 Caracteres")]
        public string? Password { get; set; }

        [DefaultValue(2)]
        public TypeUser? Profile {get; set;}
        public string? Token { get; internal set; }
    }
}