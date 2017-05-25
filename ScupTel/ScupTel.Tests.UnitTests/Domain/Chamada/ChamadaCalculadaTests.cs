using FluentAssertions;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using System;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Chamada
{
    public class ChamadaCalculadaTests
    {
        public ChamadaCalculadaTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadaCalculadaTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ChamadaCalculadaTests");

            var faleMais50 = new FaleMais("Produto FaleMais50 de Teste", 50);
            var cobertura = new Cobertura(11, 16);
            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste", cobertura, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1), 2.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var chamadaCalculada = new ChamadaCalculada(chamadaTarifada, faleMais50, 50m);

            TestConsoleWriterHelper.PrintAssertResult("A propriedade ChamadaTarifada não pode ser nula");
            chamadaCalculada.ChamadaTarifada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A propriedade ProdutoChamada não pode ser nula");
            chamadaCalculada.Produto.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O valor deve ser 50.00");
            chamadaCalculada.Valor.Should().Be(50m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}