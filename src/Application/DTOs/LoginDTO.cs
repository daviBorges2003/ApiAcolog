using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatorio!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatorio!")]
        public string? password { get; set; }
    }
}