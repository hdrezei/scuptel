using FluentAssertions;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Chamada
{
    public class ChamadaClienteTests
    {
        public ChamadaClienteTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadaClienteTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto ChamadaClienteTests");

            var cliente = new Cliente("Tony Stark");
            var cobertura = new Cobertura(11, 16);
            var inicio = DateTime.Now;
            var chamadaCliente = new ChamadaCliente(cliente, 985207410, 1132326565, cobertura, inicio, new TimeSpan(0, 20, 0));

            TestConsoleWriterHelper.PrintAssertResult("A nome do cliente deve ser 'Tony Stark'");
            chamadaCliente.Cliente.Nome.Should().Be("Tony Stark");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O numero de telefone deve ser 985207410");
            chamadaCliente.NumeroTelefoneCliente.Should().Be(985207410);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O numero de telefone discado deve ser 1132326565");
            chamadaCliente.NumeroTelefoneDiscado.Should().Be(1132326565);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A cobertura não pode ser nula");
            chamadaCliente.Cobertura.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A data de inicio deve ser " + inicio.ToString("yyyy-MM-dd hh:mm:ss"));
            chamadaCliente.Inicio.Should().Be(inicio);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A data de fim deve ser " + inicio.AddMinutes(20).ToString("yyyy-MM-dd hh:mm:ss"));
            chamadaCliente.Fim.Should().Be(inicio.AddMinutes(20));
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A duracao deve ser de 20 minutos");
            chamadaCliente.Duracao.TotalMinutes.Should().Be(20);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A propriedade ChamadasTarifadas cobertura não pode ser nula");
            chamadaCliente.ChamadasTarifadas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A propriedade ChamadasCliente não pode ser nula");
            chamadaCliente.ChamadasCliente.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
