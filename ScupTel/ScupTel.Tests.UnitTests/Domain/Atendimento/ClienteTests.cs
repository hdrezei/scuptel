using FluentAssertions;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Atendimento
{
    public class ClienteTests
    {
        public ClienteTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ClienteTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo construtor do objeto Cliente");

            var cliente = new Cliente("Tony Stark");

            TestConsoleWriterHelper.PrintAssertResult("O nome do cliente deve ser Tony Stark");
            cliente.Nome.Should().Be("Tony Stark");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve estar ativo");
            cliente.Ativo.Should().Be(true);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de numeros de telefones não pode ser nula");
            cliente.NumerosTelefone.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de produtos não pode ser nula");
            cliente.ClienteProdutosChamada.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista de chamdas não pode ser nula");
            cliente.Chamadas.Should().NotBeNull();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
