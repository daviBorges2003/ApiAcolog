using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Application.Services;
using AcologAPI.src.Domain.Entities;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AcologAPI.src.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _userServices;

        public UsersController(IUsers users)
        {
            _userServices = users;
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO )
        {
            try
            {
                var user = _userServices.Login(loginDTO);

                if(user == null)
                {
                    return Unauthorized(new {message = "O Usuario não Existe"});
                }

                return Ok(user);
            }
            
            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Erro ao realizar login", error = ex.Message});
            }
        }

        [HttpGet("/user/{id:int}")]
        public IActionResult FindOne(int id)
        {
            try
            {
                var user = _userServices.FindOne(id);
                
                if(id < 1)
                {
                    return NotFound(new {message = "Pagina não encontrada"});
                }
                if(user == null)
                {
                    return NotFound(new {message = "O Usuário não existe!"});
                }

                return Ok(user);
            }

            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Erro ao localizar usuario", error = ex.Message});
            }
        }

        [HttpGet("/users/{page:int}")]
        public IActionResult FindAll(int page)
        {
            try
            {
                var users = _userServices.FindAll(page);

                if(users == null)
                {
                    return NotFound(new {message = "Nenhum Usuário Encontrado!"});
                }

                return Ok(users);
            }

            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Erro ao localizar usuario", error = ex.Message});
            }
        }

        [HttpPost("/user")]
        
        public IActionResult Create([FromBody]Users user)
        {
            try
            {
                var newUser = _userServices.Create(user);
                var emailMessage = new EmailService("client_secret.json", "token");

                if(newUser == null)
                {
                    return NotFound(new {message = "Informações invalidas!"});
                }

                //Colocar Email, Titulo, Body
                emailMessage.SendEmail(
                    "davi.paula@hotmail.com",
                    $"Olá Senhor(a), {newUser.Name}",
                    $"<h1><{newUser.Name}/h1><br/><p>Seja muito bem vindo!</p>");
                return Created();
            }

            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Erro ao Criar usuario", error = ex.Message});
            }
        }

        [HttpPut("/user/{id:int}")]
        public IActionResult Update(int id,[FromBody]Users user)
        {   
            try
            {
                var findUser = _userServices.FindOne(id);

                if(findUser == null)
                {
                    return NotFound(new {message = "Usuário não encontrado!" });
                }

                findUser.Name = user.Name ?? findUser.Name;
                findUser.Email = user.Email ?? findUser.Email;
                findUser.Password = user.Password ?? findUser.Password;
                findUser.Profile = user.Profile ?? findUser.Profile;

                _userServices.Update(findUser);

                return Ok(findUser);
            }

            catch(Exception ex)
            {
                return StatusCode(304, new {message = "Erro ao Criar usuario", error = ex.Message});
            }            
        }
        
        [HttpDelete("/user/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var findUser = _userServices.FindOne(id);

                if(findUser == null)
                {
                    return NotFound(new {message = "Usuário não encontrado!" });
                }
                _userServices.Delete(findUser);

                return Ok(new { message = "Useuario deletado!!"});
            }

            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Erro ao Criar usuario", error = ex.Message});
            }        
        }
    }
}