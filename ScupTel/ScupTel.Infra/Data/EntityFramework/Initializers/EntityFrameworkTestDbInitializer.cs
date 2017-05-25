using Microsoft.EntityFrameworkCore;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.Data.EntityFramework.Repositories;
using System;
using System.Linq;

namespace ScupTel.Infra.Data.EntityFramework.Initializers
{
    public static class EntityFrameworkTestDbInitializer
    {
        public static void Initialize(ScupTelDbContext testDbContext)
        {
            testDbContext.Database.EnsureCreated();

            // Look for any students.
            if (testDbContext.Coberturas.Any())
            {
                return;   // DB has been seeded
            }

            var coberturas = new Cobertura[]
            {
                new Cobertura(11, 16),
                new Cobertura(16, 11),
                new Cobertura(11, 17),
                new Cobertura(17, 11),
                new Cobertura(11, 18),
                new Cobertura(18, 11)
            };

            testDbContext.AddRange(coberturas);
            
            var tarifasChamada = new TarifaChamada[]
            {
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > Ribeirão Preto", coberturas[0], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.90m),
                new LongaDistanciaNacional("Tarifa Normal - Ribeirão Preto > São Paulo", coberturas[1], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 2.90m),
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > S. José do Rio Preto", coberturas[2], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.70m),
                new LongaDistanciaNacional("Tarifa Normal - S. José do Rio Preto > São Paulo", coberturas[3], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 2.70m),
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > Presidente Prudente", coberturas[4], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 0.90m),
                new LongaDistanciaNacional("Tarifa Normal - Presidente Prudente > São Paulo", coberturas[5], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.90m)
            };

            testDbContext.TarifasChamada.AddRange(tarifasChamada);

            var produtos = new ProdutoChamada[]
            {
                new PlanoBasico("Plano Básico"),
                new FaleMais("FaleMais 30", 30),
                new FaleMais("FaleMais 60", 60),
                new FaleMais("FaleMais 120", 120),
            };

            testDbContext.ProdutosChamada.AddRange(produtos);
            
            var clientes = new Cliente[]
            {
                new Cliente("Peter Parker"),
                new Cliente("Steve Rogers"),
                new Cliente("Tony Stark"),
                new Cliente("Phil Coulson")
            };

            testDbContext.Clientes.AddRange(clientes);

            var clientesProdutosChamada = new ClienteProdutoChamadaRelation[]
            {
                new ClienteProdutoChamadaRelation(clientes[0], produtos[0], true),
                new ClienteProdutoChamadaRelation(clientes[1], produtos[1], true),
                new ClienteProdutoChamadaRelation(clientes[2], produtos[2], true),
                new ClienteProdutoChamadaRelation(clientes[3], produtos[3], false)
            };

            clientes[0].ClienteProdutosChamada.Add(clientesProdutosChamada[0]);
            clientes[1].ClienteProdutosChamada.Add(clientesProdutosChamada[1]);
            clientes[2].ClienteProdutosChamada.Add(clientesProdutosChamada[2]);
            clientes[3].ClienteProdutosChamada.Add(clientesProdutosChamada[3]);
            
            var telefones = new Telefone[]
            {
                new Telefone(987456321, 11, 55, clientes[0]),
                new Telefone(975486235, 16, 55, clientes[1]),
                new Telefone(998467852, 17, 55, clientes[2]),
                new Telefone(946131728, 21, 55, clientes[3])
            };

            testDbContext.Telefones.AddRange(telefones);

            var chamadas = new ChamadaBase[]
            {
                new ChamadaRealizada(coberturas[0], DateTime.Parse("2017-01-29 14:00:00.000"), new TimeSpan(0, 20, 0)),
                new ChamadaRealizada(coberturas[2], DateTime.Parse("2017-01-30 08:30:00.000"), new TimeSpan(0, 80, 0)),
                new ChamadaCliente(clientes[0], telefones[0].Numero, telefones[1].Numero, coberturas[5], DateTime.Parse("2017-01-31 07:45:00.000"), new TimeSpan(0, 200, 0))
            };
            
            var chamadasTarifadas = new ChamadaTarifada[]
            {
                new ChamadaTarifada(chamadas[0]),
                new ChamadaTarifada(chamadas[1]),
                new ChamadaTarifada(chamadas[2])
            };

            var resumoCalculotarifaChamada = new ResumoCalculoTarifaChamada[] 
            {
                tarifasChamada[0].TarifaCalculada(chamadasTarifadas[0]),
                tarifasChamada[1].TarifaCalculada(chamadasTarifadas[1]),
                tarifasChamada[2].TarifaCalculada(chamadasTarifadas[2])
            };

            chamadasTarifadas[0].Tarifas.Add(resumoCalculotarifaChamada[0]);
            chamadasTarifadas[1].Tarifas.Add(resumoCalculotarifaChamada[1]);
            chamadasTarifadas[2].Tarifas.Add(resumoCalculotarifaChamada[2]);

            testDbContext.ChamadasTarifadas.AddRange(chamadasTarifadas);
            testDbContext.SaveChanges();
        }
    }
}