using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //TODO: 31/01/2025

        //Users Create (Users user); // Create
        //Users Update (Users user); //Atualizar
        //Users Delete (Users user); // Deletar
    }
}