using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;
using Infrastructure.Persistence;
using Application.DTOs;
using Application.Interfaces;
using AcologAPI.src.Presentation.Controllers;

namespace Application.Services
{
    public class UserServices : IUsers
    {
        private readonly MyDbContext _context;

        public UserServices(MyDbContext db)
        {
            _context = db;
        }

        public Users? Login(LoginDTO loginDTO)
        {
            var adm = _context.Users.Where(
                res => res.Email == loginDTO.Email && res.PasswordHash == loginDTO.PasswordHash
            ).FirstOrDefault();

            return adm;
        }

        public Users? FindOne(int id)
        {
            return _context.Users.Where(res => res.Id == id).FirstOrDefault();
        }

        public List<Users> FindAll(int? pagina)
        {
            var query = _context.Users.AsQueryable();

            int itemsPerPage = 10;

            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itemsPerPage).Take(itemsPerPage);
            } 

            return query.ToList(); 
        }

        public Users Create(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Users user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
 
        public void Delete(Users user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

    }
}