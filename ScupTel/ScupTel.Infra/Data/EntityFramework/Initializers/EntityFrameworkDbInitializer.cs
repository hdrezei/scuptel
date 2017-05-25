using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.Data.EntityFramework.Repositories;
using System;
using System.Linq;

namespace ScupTel.Infra.Data.EntityFramework.Initializers
{
    public static class EntityFrameworkDbInitializer
    {
        public static void Initialize(ScupTelDbContext context)
        {
            // Look for any students.
            if (context.Coberturas.Any())
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

            context.AddRange(coberturas);
            
            var tarifasChamada = new TarifaChamada[]
            {
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > Ribeirão Preto", coberturas[0], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.90m),
                new LongaDistanciaNacional("Tarifa Normal - Ribeirão Preto > São Paulo", coberturas[1], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 2.90m),
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > S. José do Rio Preto", coberturas[2], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.70m),
                new LongaDistanciaNacional("Tarifa Normal - S. José do Rio Preto > São Paulo", coberturas[3], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 2.70m),
                new LongaDistanciaNacional("Tarifa Normal - São Paulo > Presidente Prudente", coberturas[4], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 0.90m),
                new LongaDistanciaNacional("Tarifa Normal - Presidente Prudente > São Paulo", coberturas[5], DateTime.Parse("2017-01-01 00:00:00.000"), DateTime.Parse("2017-12-31 23:59:59.999"), 1.90m)
            };

            context.TarifasChamada.AddRange(tarifasChamada);

            var produtos = new ProdutoChamada[]
            {
                new PlanoBasico("Plano Básico"),
                new FaleMais("FaleMais 30", 30),
                new FaleMais("FaleMais 60", 60),
                new FaleMais("FaleMais 120", 120),
            };

            context.ProdutosChamada.AddRange(produtos);
            context.SaveChanges();
        }
    }
}