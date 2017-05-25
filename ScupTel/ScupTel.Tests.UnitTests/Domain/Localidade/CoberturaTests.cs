using ScupTel.Domain.Localidade;
using FluentAssertions;
using Xunit;
using ScupTel.Domain.Tarifa;
using System.Collections.Generic;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;

namespace ScupTel.Tests.UnitTests.Domain.Localidade
{
    public class CoberturaTests
    {
        public CoberturaTests()
        {
            TestConsoleWriterHelper.PrintTestClass("CoberturaTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto Cobertura");

            var cobertura = new Cobertura(11, 12);

            TestConsoleWriterHelper.PrintAssertResult("Ddd de origem deve ser 11");
            cobertura.DddOrigem.Should().BeOfType(typeof(int)).And.Be(11);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Ddd de destino deve ser 12");
            cobertura.DddDestino.Should().BeOfType(typeof(int)).And.Be(12);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de chamadas não deve ser nula");
            cobertura.Chamadas.Should().BeOfType(typeof(List<ChamadaBase>)).And.NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de tarifas não pode ser nula");
            cobertura.TarifasChamada.Should().BeOfType(typeof(List<TarifaChamada>)).And.NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
