using FluentAssertions;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Chamada
{
    public class ChamadaRealizadaTests
    {
        public ChamadaRealizadaTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadaRealizadaTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ChamadaRealizadaTests");

            var cobertura = new Cobertura(11, 16);
            var inicio = DateTime.Now;
            var chamadaRealizada = new ChamadaRealizada(cobertura, inicio, new TimeSpan(0, 20, 0));

            TestConsoleWriterHelper.PrintAssertResult("A cobertura não pode ser nula");
            chamadaRealizada.Cobertura.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A data de inicio deve ser " + inicio.ToString("yyyy-MM-dd hh:mm:ss"));
            chamadaRealizada.Inicio.Should().Be(inicio);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A data de fim deve ser " + inicio.AddMinutes(20).ToString("yyyy-MM-dd hh:mm:ss"));
            chamadaRealizada.Fim.Should().Be(inicio.AddMinutes(20));
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A duracao deve ser de 20 minutos");
            chamadaRealizada.Duracao.TotalMinutes.Should().Be(20);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A propriedade ChamadasTarifadas cobertura não pode ser nula");
            chamadaRealizada.ChamadasTarifadas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A propriedade ChamadasCliente não pode ser nula");
            chamadaRealizada.ChamadasCliente.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
