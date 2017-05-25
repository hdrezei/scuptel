using FluentAssertions;
using Moq;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Produto;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Atendimento
{
    public class ClienteServiceTests
    {
        public ClienteServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ClienteServiceTests");
        }

        [Fact]
        public void TestaMetodoGet()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Get");

            var mockClienteRepository = new Mock<IClienteRepository>();
            var identifier = Guid.NewGuid();

            mockClienteRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var produto = new FaleMais("FaleMais 50", 50);
                    var cliente = new Cliente("Tony Stark")
                    {
                        Id = identifier
                    };

                    cliente.NumerosTelefone.Add(new Telefone(985207410, 11, 55, cliente));
                    cliente.ClienteProdutosChamada.Add(new ClienteProdutoChamadaRelation(cliente, produto, true));

                    return cliente;
                });

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.Get(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve ter um Id válido");
            result.Id.Should().Be(identifier);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve ter o nome 'Tony Stark'");
            result.Nome.Should().Be("Tony Stark");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve estar ativo");
            result.Ativo.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve ter 1 numero de telefone");
            result.NumerosTelefone.Count.Should().Be(1);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O numero de telefone deve ser 985207410");
            result.NumerosTelefone.Single().Numero.Should().Be(985207410);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente deve ter um produto ativo");
            result.ClienteProdutosChamada.Count(a => a.Ativo == true).Should().Be(1);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O cliente nao deve ter chamadas realizadas");
            result.Chamadas.Count.Should().Be(0);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo All");

            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.All())
                .Returns(() =>
                {
                    var clientes = new Cliente[]
                    {
                        new Cliente("Peter Parker"),
                        new Cliente("Steve Rogers"),
                        new Cliente("Tony Stark"),
                        new Cliente("Phil Coulson")
                    };
                    
                    return clientes;
                });

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.All();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 4 itens");
            result.Count().Should().Be(4);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 1 cliente com o nome Peter Parker");
            result.Count(o => o.Nome.Equals("Peter Parker", StringComparison.CurrentCultureIgnoreCase)).Should().Be(1);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFind()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Find");
            
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .Returns(() =>
                {
                    var clientes = new Cliente[]
                    {
                        new Cliente("Peter Parker"),
                        new Cliente("Steve Rogers"),
                        new Cliente("Tony Stark"),
                        new Cliente("Phil Coulson")
                    };

                    return clientes.Single(o => o.Nome.Equals("Peter Parker", StringComparison.CurrentCultureIgnoreCase));
                });

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.Find(o => o.Nome.Equals("Peter Parker", StringComparison.CurrentCultureIgnoreCase));

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 1 cliente com o nome Peter Parker");
            result.Nome.Equals("Peter Parker", StringComparison.CurrentCultureIgnoreCase).Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFindAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo FindAll retornando somente clientes ativos");

            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.FindAll(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .Returns(() =>
                {
                    var clientes = new Cliente[]
                    {
                        new Cliente("Peter Parker"),
                        new Cliente("Steve Rogers"),
                        new Cliente("Tony Stark"),
                        new Cliente("Phil Coulson")
                    };

                    clientes[0].Ativo = false;
                    clientes[2].Ativo = false;

                    return clientes.Where(o => o.Ativo == true).ToList();
                });

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.FindAll(o => o.Ativo == true);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 1 cliente com o nome Peter Parker");
            result.Count().Should().Be(2);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteCliente()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um cliente");
            
            var cliente = new Cliente("Tony Stark");
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.Delete(It.IsAny<Cliente>()))
                .Returns(true);

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.Delete(cliente);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteClientePeloIdentifier()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um identifier de cliente");

            var identifier = Guid.NewGuid();
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(true);

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.Delete(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSave()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Save cliente");

            var cliente = new Cliente("Tony Stark");
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.Save(It.IsAny<Cliente>()))
                .Returns(() =>
                {
                    cliente.Id = Guid.NewGuid();
                    return cliente;
                });

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.Save(cliente);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar Cliente com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAdquirirNumeroTelefone()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo AdquirirNumeroTelefone");

            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(985207410, 11, 55, null);
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.AdquirirNumeroTelefone(It.IsAny<Cliente>(), It.IsAny<Telefone>()))
                .Returns(true);

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.AdquirirNumeroTelefone(cliente, telefone);

            TestConsoleWriterHelper.PrintAssertResult("O resultado do metodo deve ser verdadeiro");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAdquirirProduto()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo AdquirirProduto");

            var cliente = new Cliente("Tony Stark");
            var produto = new FaleMais("FaleMais 50", 50);
            var mockClienteRepository = new Mock<IClienteRepository>();

            mockClienteRepository
                .Setup(x => x.AdquirirProduto(It.IsAny<Cliente>(), It.IsAny<ProdutoChamada>()))
                .Returns(true);

            var clienteService = new ClienteService(mockClienteRepository.Object);
            var result = clienteService.AdquirirProduto(cliente, produto);

            TestConsoleWriterHelper.PrintAssertResult("O resultado do metodo deve ser verdadeiro");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
