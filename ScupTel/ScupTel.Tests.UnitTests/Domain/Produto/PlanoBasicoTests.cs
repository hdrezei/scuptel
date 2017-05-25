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
    public class PlanoBasicoTests
    {
        public PlanoBasicoTests()
        {
            TestConsoleWriterHelper.PrintTestClass("PlanoBasicoTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto PlanoBasico");

            var planoBasico = new PlanoBasico("Produto de Teste");

            TestConsoleWriterHelper.PrintAssertResult("O nome do produto deve ser 'Produto de Teste'");
            planoBasico.Nome.Should().Be("Produto de Teste");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de clienteProdutosChamada não pode ser nula");
            planoBasico.ClienteProdutosChamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoCalculoChamada()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo CalculoChamada no produto PlanoBasico");

            var planoBasico = new PlanoBasico("Produto de Teste");
            var cobertura = new Cobertura(11, 16);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 1.25m);
            var resumoCalculoTarifaChamada = tarifaChamada.TarifaCalculada(chamadaTarifada);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada);

            var chamadaCalculada = planoBasico.CalculoChamada(chamadaTarifada, 2);
            
            TestConsoleWriterHelper.PrintAssertResult("O tempo total de ligação é 20 minutos");
            chamadaCalculada.ChamadaTarifada.Chamada.Duracao.TotalMinutes.Should().Be(20.00);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor total da tarifa é 1.25");
            chamadaCalculada.ChamadaTarifada.Total().Should().Be(1.25m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O resultado do calculo no produto PlanoBasico deve ser 25.00");
            chamadaCalculada.Valor.Should().Be(25.00m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
