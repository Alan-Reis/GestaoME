using ClickServ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ.Repository
{
    public class ClickServContext : DbContext
    {
        public ClickServContext(DbContextOptions<ClickServContext> options) : base (options)
        {

        }

        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<Colaborador> Colaboradors { get; set; }
        public DbSet<Condominio> Condominios { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<OrdemServico> OrdemServicos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendimento>().ToTable("Atendimento");
            modelBuilder.Entity<Colaborador>().ToTable("Colaborador");
            modelBuilder.Entity<Condominio>().ToTable("Condominio");
            modelBuilder.Entity<Contato>().ToTable("Contato");
            modelBuilder.Entity<Endereco>().ToTable("Endereco");
            modelBuilder.Entity<Equipamento>().ToTable("Equipamento");
            modelBuilder.Entity<OrdemServico>().ToTable("OrdemServico");
            modelBuilder.Entity<Pessoa>().ToTable("Pessoa");

        }
    }
}
