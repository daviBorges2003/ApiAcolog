using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcologAPI.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcologAPI.src.Infrastructure.Persistence
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {}

        public DbSet<Users> Users {get; set;}
        public DbSet<Vehicle> Vehicles {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().HasKey(u => u.Id);
        }
    }
}