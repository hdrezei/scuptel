using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using ScupTel.Infra.Data.EntityFramework.Mappings;
using System;
using System.IO;
using System.Linq;

namespace ScupTel.Infra.Data.EntityFramework.Context
{
    public class ScupTelDbContext : DbContext
    {
        public ScupTelDbContext(DbContextOptions<ScupTelDbContext> options) 
            : base(options)
        {
        }

        // Domain - Atendimento
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteProdutoChamadaRelation> ClientesProdutosChamada { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        // Domain - Chamada
        public DbSet<ChamadaBase> Chamadas { get; set; }
        public DbSet<ChamadaCliente> ChamadasCliente { get; set; }
        public DbSet<ChamadaRealizada> ChamadasRealizadas { get; set; }
        public DbSet<ChamadaTarifada> ChamadasTarifadas { get; set; }

        // Domain - Cobertura
        public DbSet<Cobertura> Coberturas { get; set; }

        // Domain - Produto
        public DbSet<FaleMais> FaleMais { get; set; }
        public DbSet<PlanoBasico> PlanoBasico { get; set; }
        public DbSet<ProdutoChamada> ProdutosChamada { get; set; }

        // Domain - Tarifa
        public DbSet<LongaDistanciaNacional> TarifasLongaDistanciaNacional { get; set; }
        public DbSet<ResumoCalculoTarifaChamada> ResumoCalculoTarifasChamada { get; set; }
        public DbSet<TarifaChamada> TarifasChamada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Domain - Atendimento
            modelBuilder.AddConfiguration(new ClienteMap());
            modelBuilder.AddConfiguration(new ClienteProdutoChamadaRelationMap());
            modelBuilder.AddConfiguration(new TelefoneMap());

            // Domain - Chamada
            modelBuilder.AddConfiguration(new ChamadaBaseMap());
            modelBuilder.AddConfiguration(new ChamadaClienteMap());
            modelBuilder.AddConfiguration(new ChamadaTarifadaMap());

            // Domain - Cobertura
            modelBuilder.AddConfiguration(new CoberturaMap());

            // Domain - Produto
            modelBuilder.AddConfiguration(new ProdutoChamadaMap());

            // Domain - Tarifa
            modelBuilder.AddConfiguration(new TarifaChamadaMap());
            modelBuilder.AddConfiguration(new ResumoCalculoTarifaChamadaMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Property("DataDeRegistro") != null))
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property("DataDeRegistro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataDeRegistro").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Property("Id") == null))
            {
                if (entry.Property("Id").CurrentValue == ((object)Guid.Empty))
                {
                    entry.Property("Id").CurrentValue = Guid.NewGuid();
                }
            }

            return base.SaveChanges();
        }
    }
}
