using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoComercial.Web.Models;

namespace GestaoComercial.Web.Data
{
    public class GestaoComercialContext : DbContext
    {

        public GestaoComercialContext() { }

        public GestaoComercialContext(DbContextOptions<GestaoComercialContext> options) : base(options)
        {
        }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>().ToTable("Conta");
            modelBuilder.Entity<Compra>().ToTable("Compra");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
        }
    }
}
