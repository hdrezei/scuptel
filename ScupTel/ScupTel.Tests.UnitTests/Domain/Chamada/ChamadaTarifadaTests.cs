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

namespace ScupTel.Tests.UnitTests.Domain.Chamada
{
    public class ChamadaTarifadaTests
    {
        public ChamadaTarifadaTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadaTarifadaTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ChamadaTarifadaTests");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            TestConsoleWriterHelper.PrintAssertResult("A propriedade Chamada não pode ser nula");
            chamadaTarifada.Chamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A propriedade Tarifas não pode ser nula");
            chamadaTarifada.Chamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoTotal()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ChamadaTarifadaTests");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada1 = new LongaDistanciaNacional("Tarifa de Teste 1", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2.25m);
            var tarifaChamada2 = new LongaDistanciaNacional("Tarifa de Teste 2", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 6.50m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada1 = tarifaChamada1.TarifaCalculada(chamadaTarifada);
            var resumoCalculoTarifaChamada2 = tarifaChamada2.TarifaCalculada(chamadaTarifada);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada1);
            chamadaTarifada.Tarifas.Add(resumoCalculoTarifaChamada2);

            TestConsoleWriterHelper.PrintAssertResult("A propriedade Tarifas deve contem 2 items");
            chamadaTarifada.Tarifas.Count.Should().Be(2);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O valor Total deve deve ser 8.75");
            chamadaTarifada.Total().Should().Be(8.75m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}