using FluentAssertions;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Atendimento
{
    public class TelefoneTests
    {
        public TelefoneTests()
        {
            TestConsoleWriterHelper.PrintTestClass("TelefoneTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto Telefone");

            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(974108520, 11, 55, cliente);

            TestConsoleWriterHelper.PrintAssertResult("O proprietário do numero deve ser Tony Stark");
            telefone.Proprietario.Nome.Should().Be("Tony Stark");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O numero deve ser 974108520");
            telefone.Numero.Should().Be(974108520);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O ddd de registro deve ser 11");
            telefone.DddRegistro.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O ddi de registro deve ser 55");
            telefone.DdiRegistro.Should().Be(55);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de chamadas realizadas não pode ser nula");
            telefone.ChamadasRealizadas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A lista de chamadas recebidas não pode ser nula");
            telefone.ChamadasRecebidas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
