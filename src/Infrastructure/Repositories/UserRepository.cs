using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Application.Interfaces;
using AcologAPI.src.Domain.Entities;
using Application.DTOs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcologAPI.src.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context; 
        }

        public async Task<Users> Create(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Users> Update(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Users> Delete(Users user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<Users>> FindAll(int pagina)
        {
                    // {
            var query = _context.Users.AsQueryable();

            int itemsPerPage = 10;

            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itemsPerPage).Take(itemsPerPage);
            } 

            return await query.ToListAsync(); 
        }

        public async Task<Users> FindOne(int id)
        {
            return await _context.Users.Where(res => res.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> Login(string email, string password)
        {
            return await _context.Users.Where(res => res.Email == email).FirstOrDefaultAsync();
        }
    }
}