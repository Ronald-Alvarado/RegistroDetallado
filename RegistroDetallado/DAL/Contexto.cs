using Microsoft.EntityFrameworkCore;
using RegistroDetallado.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroDetallado.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA/Persona.db");
        }
    }
}
