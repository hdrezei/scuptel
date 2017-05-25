using FluentAssertions;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Produto
{
    public class FaleMaisTests
    {
        public FaleMaisTests()
        {
            TestConsoleWriterHelper.PrintTestClass("FaleMaisTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto FaleMais");

            var faleMais10 = new FaleMais("Produto FaleMais10 de Teste", 10);

            TestConsoleWriterHelper.PrintAssertResult("O nome do produto deve ser 'Produto FaleMais10 de Teste'");
            faleMais10.Nome.Should().Be("Produto FaleMais10 de Teste");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor dos minutos de franquia deve ser '10'");
            faleMais10.MinutosFranquia.Should().Be(10);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de clienteProdutosChamada não pode ser nula");
            faleMais10.ClienteProdutosChamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoCalculoChamadaIsentaDeCobranca()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo CalculoChamada do produto FaleMais com chamada isenta de cobrança");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = tarifaChamada.TarifaCalculada(chamadaTarifada);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada);

            var chamadaCalculada = faleMais50.CalculoChamada(chamadaTarifada, 2);

            TestConsoleWriterHelper.PrintAssertResult("O tempo total de ligação é 20 minutos");
            chamadaCalculada.ChamadaTarifada.Chamada.Duracao.TotalMinutes.Should().Be(20.00);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor total da tarifa é 2.25");
            chamadaCalculada.ChamadaTarifada.Total().Should().Be(2.25m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O resultado do calculo no produto 'FaleMais50 de Teste' deve ser 0.00");
            chamadaCalculada.Valor.Should().Be(0.00m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoTempoDeChamadaTarifado()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo TempoDeChamadaTarifado do produto FaleMais");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 20);
            var cobertura = new Cobertura(11, 16);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 54, 20));

            TestConsoleWriterHelper.PrintAssertResult("O tempo total que será cobrado é 34.33");
            var tempoDeChamadaTarifada = faleMais50.TempoDeChamadaTarifado(chamadaRealizada);
            tempoDeChamadaTarifada.Should().Be(34.33);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoValorTotalDaTarifa()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo ValorTotalDaTarifa do produto FaleMais");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 3.75m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 85, 36));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = tarifaChamada.TarifaCalculada(chamadaTarifada);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada);

            TestConsoleWriterHelper.PrintAssertResult("O valor total da tarifa com percentual adicional é 4.125");
            var valorTotalTarifa = faleMais50.ValorTotalDaTarifa(chamadaTarifada);
            valorTotalTarifa.Should().Be(4.125m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoCalculoChamadaTarifada()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo CalculoChamada do produto FaleMais com chamada tarifada");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 85, 36));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = tarifaChamada.TarifaCalculada(chamadaTarifada);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada);

            var chamadaCalculada = faleMais50.CalculoChamada(chamadaTarifada, 2);

            TestConsoleWriterHelper.PrintAssertResult("O tempo total de ligação é 85 minutos e 36 segundos");
            Math.Round(chamadaCalculada.ChamadaTarifada.Chamada.Duracao.TotalMinutes, 2).Should().Be(85.6);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O tempo total que será cobrado é 35.6");
            var tempoDeChamadaTarifada = faleMais50.TempoDeChamadaTarifado(chamadaTarifada.Chamada);
            tempoDeChamadaTarifada.Should().Be(35.6);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor total da tarifa com percentual adicional é 2.475");
            var valorTotalTarifa = faleMais50.ValorTotalDaTarifa(chamadaTarifada);
            valorTotalTarifa.Should().Be(2.475m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O resultado do calculo no produto 'FaleMais50 de Teste' deve ser 88.11");
            chamadaCalculada.Valor.Should().Be(88.11m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
