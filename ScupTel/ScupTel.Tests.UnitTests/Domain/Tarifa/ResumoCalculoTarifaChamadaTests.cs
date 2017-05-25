using FluentAssertions;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Tarifa
{
    public class ResumoCalculoTarifaChamadaTests
    {
        public ResumoCalculoTarifaChamadaTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ResumoCalculoTarifaChamadaTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ResumoCalculoTarifaChamada");

            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m);
            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            var resumoCalculoTarifaChamada = new ResumoCalculoTarifaChamada(chamadaTarifada, longaDistanciaNacional, longaDistanciaNacional.CalcularTarifaChamada(longaDistanciaNacional), true);

            TestConsoleWriterHelper.PrintAssertResult("A ChamadaTarifada nao deve ser nula");
            resumoCalculoTarifaChamada.ChamadaTarifada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A TarifaChamada nao deve ser nula");
            resumoCalculoTarifaChamada.TarifaChamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O valor dever ser '1.25'");
            resumoCalculoTarifaChamada.Valor.Should().Be(1.25m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A propriedade 'aplicado' dever ser verdadeiro");
            resumoCalculoTarifaChamada.Aplicado.Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
