using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VuelaLibre.Models.Maps;

namespace VuelaLibre.Models
{
    public class VuelaLibreContext : DbContext
    {
        //Esto se hace por cada tabla de base de datos
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vuelos> Vuelo { get; set; }

        public VuelaLibreContext(DbContextOptions<VuelaLibreContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Esto se hace por cada tabla de base de datos
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new VueloMap());
        }
    }
}
