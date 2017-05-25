using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ScupTel.Infra.Data.EntityFramework.Context;

namespace ScupTel.Infra.Data.EntityFramework.Migrations
{
    [DbContext(typeof(ScupTelDbContext))]
    partial class ScupTelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ScupTel.Domain.Atendimento.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit")
                        .HasMaxLength(1);

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ScupTel.Domain.Atendimento.ClienteProdutoChamadaRelation", b =>
                {
                    b.Property<Guid>("ClienteId");

                    b.Property<Guid>("ProdutoChamadaId");

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<Guid>("Id");

                    b.HasKey("ClienteId", "ProdutoChamadaId");

                    b.HasIndex("ProdutoChamadaId");

                    b.ToTable("ClientesProdutosChamada");
                });

            modelBuilder.Entity("ScupTel.Domain.Atendimento.Telefone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClienteId");

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<int>("DddRegistro");

                    b.Property<int>("DdiRegistro");

                    b.Property<int>("Numero");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CoberturaId");

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("Fim");

                    b.Property<DateTime>("Inicio");

                    b.HasKey("Id");

                    b.HasIndex("CoberturaId");

                    b.ToTable("Chamadas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ChamadaBase");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaTarifada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChamadaRealizadaId");

                    b.Property<DateTime>("DataDeRegistro");

                    b.HasKey("Id");

                    b.HasIndex("ChamadaRealizadaId");

                    b.ToTable("ChamadasTarifadas");
                });

            modelBuilder.Entity("ScupTel.Domain.Localidade.Cobertura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<int>("DddDestino");

                    b.Property<int>("DddOrigem");

                    b.HasKey("Id");

                    b.HasIndex("DddOrigem", "DddDestino")
                        .IsUnique();

                    b.ToTable("Coberturas");
                });

            modelBuilder.Entity("ScupTel.Domain.Produto.ProdutoChamada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("ProdutosChamada");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProdutoChamada");
                });

            modelBuilder.Entity("ScupTel.Domain.Tarifa.ResumoCalculoTarifaChamada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aplicado");

                    b.Property<Guid>("ChamadaTarifadaId");

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<Guid>("TarifaChamadaId");

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ChamadaTarifadaId");

                    b.HasIndex("TarifaChamadaId");

                    b.ToTable("ResumoCalculoTarifasChamada");
                });

            modelBuilder.Entity("ScupTel.Domain.Tarifa.TarifaChamada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CoberturaId");

                    b.Property<DateTime>("DataDeRegistro");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("FimVigencia");

                    b.Property<DateTime>("InicioVigencia");

                    b.Property<string>("Nome");

                    b.Property<decimal>("ValorBaseCalculo");

                    b.HasKey("Id");

                    b.HasIndex("CoberturaId");

                    b.ToTable("TarifasChamada");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TarifaChamada");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaCliente", b =>
                {
                    b.HasBaseType("ScupTel.Domain.Chamada.ChamadaBase");

                    b.Property<Guid?>("ChamadaBaseId");

                    b.Property<Guid>("ClienteId");

                    b.Property<int>("NumeroTelefoneCliente");

                    b.Property<int>("NumeroTelefoneDiscado");

                    b.Property<Guid?>("TelefoneId");

                    b.Property<Guid?>("TelefoneId1");

                    b.HasIndex("ChamadaBaseId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("TelefoneId");

                    b.HasIndex("TelefoneId1");

                    b.ToTable("ChamadaCliente");

                    b.HasDiscriminator().HasValue("ChamadaCliente");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaRealizada", b =>
                {
                    b.HasBaseType("ScupTel.Domain.Chamada.ChamadaBase");


                    b.ToTable("ChamadaRealizada");

                    b.HasDiscriminator().HasValue("ChamadaRealizada");
                });

            modelBuilder.Entity("ScupTel.Domain.Produto.FaleMais", b =>
                {
                    b.HasBaseType("ScupTel.Domain.Produto.ProdutoChamada");

                    b.Property<int>("MinutosFranquia");

                    b.ToTable("FaleMais");

                    b.HasDiscriminator().HasValue("FaleMais");
                });

            modelBuilder.Entity("ScupTel.Domain.Produto.PlanoBasico", b =>
                {
                    b.HasBaseType("ScupTel.Domain.Produto.ProdutoChamada");


                    b.ToTable("PlanoBasico");

                    b.HasDiscriminator().HasValue("PlanoBasico");
                });

            modelBuilder.Entity("ScupTel.Domain.Tarifa.LongaDistanciaNacional", b =>
                {
                    b.HasBaseType("ScupTel.Domain.Tarifa.TarifaChamada");


                    b.ToTable("LongaDistanciaNacional");

                    b.HasDiscriminator().HasValue("LongaDistanciaNacional");
                });

            modelBuilder.Entity("ScupTel.Domain.Atendimento.ClienteProdutoChamadaRelation", b =>
                {
                    b.HasOne("ScupTel.Domain.Atendimento.Cliente", "Cliente")
                        .WithMany("ClienteProdutosChamada")
                        .HasForeignKey("ClienteId");

                    b.HasOne("ScupTel.Domain.Produto.ProdutoChamada", "ProdutoChamada")
                        .WithMany("ClienteProdutosChamada")
                        .HasForeignKey("ProdutoChamadaId");
                });

            modelBuilder.Entity("ScupTel.Domain.Atendimento.Telefone", b =>
                {
                    b.HasOne("ScupTel.Domain.Atendimento.Cliente", "Proprietario")
                        .WithMany("NumerosTelefone")
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaBase", b =>
                {
                    b.HasOne("ScupTel.Domain.Localidade.Cobertura", "Cobertura")
                        .WithMany("Chamadas")
                        .HasForeignKey("CoberturaId");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaTarifada", b =>
                {
                    b.HasOne("ScupTel.Domain.Chamada.ChamadaBase", "Chamada")
                        .WithMany("ChamadasTarifadas")
                        .HasForeignKey("ChamadaRealizadaId");
                });

            modelBuilder.Entity("ScupTel.Domain.Tarifa.ResumoCalculoTarifaChamada", b =>
                {
                    b.HasOne("ScupTel.Domain.Chamada.ChamadaTarifada", "ChamadaTarifada")
                        .WithMany("Tarifas")
                        .HasForeignKey("ChamadaTarifadaId");

                    b.HasOne("ScupTel.Domain.Tarifa.TarifaChamada", "TarifaChamada")
                        .WithMany("ResumoTarifasAplicadas")
                        .HasForeignKey("TarifaChamadaId");
                });

            modelBuilder.Entity("ScupTel.Domain.Tarifa.TarifaChamada", b =>
                {
                    b.HasOne("ScupTel.Domain.Localidade.Cobertura", "Cobertura")
                        .WithMany("TarifasChamada")
                        .HasForeignKey("CoberturaId");
                });

            modelBuilder.Entity("ScupTel.Domain.Chamada.ChamadaCliente", b =>
                {
                    b.HasOne("ScupTel.Domain.Chamada.ChamadaBase")
                        .WithMany("ChamadasCliente")
                        .HasForeignKey("ChamadaBaseId");

                    b.HasOne("ScupTel.Domain.Atendimento.Cliente", "Cliente")
                        .WithMany("Chamadas")
                        .HasForeignKey("ClienteId");

                    b.HasOne("ScupTel.Domain.Atendimento.Telefone")
                        .WithMany("ChamadasRealizadas")
                        .HasForeignKey("TelefoneId");

                    b.HasOne("ScupTel.Domain.Atendimento.Telefone")
                        .WithMany("ChamadasRecebidas")
                        .HasForeignKey("TelefoneId1");
                });
        }
    }
}
