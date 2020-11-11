using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DAL
{
    public class Contexto :DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=DATA\Prestamos.db");
        }
    }
}
