using FluentAssertions;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Produto;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Atendimento
{
    public class ClienteProdutoChamadaRelationTests
    {
        public ClienteProdutoChamadaRelationTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ClienteProdutoChamadaRelationTests");
        }

        [Fact]
        public void TestaConstrutorDoObjeto()
        {
            TestConsoleWriterHelper.PrintTestClass("Testa o metodo construtor do objeto ClienteProdutoChamadaRelation");

            var cliente = new Cliente("Tony Stark");
            var produtoChamada = new PlanoBasico("Produto de Teste");

            var clienteProdutoChamadaRelation = new ClienteProdutoChamadaRelation(cliente, produtoChamada, true);

            TestConsoleWriterHelper.PrintAssertResult("O nome do cliente relacionado deve ser 'Tony Stark'");
            clienteProdutoChamadaRelation.Cliente.Nome.Should().Be("Tony Stark");
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("O nome do produto relacionado deve ser 'Produto de Teste'");
            clienteProdutoChamadaRelation.ProdutoChamada.Nome.Should().Be("Produto de Teste");
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A relação entre o produto eo cliente deve esta 'ativa'");
            clienteProdutoChamadaRelation.Ativo.Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
