using FluentAssertions;
using Moq;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Produto
{
    public class ProdutoChamadaServiceTests
    {
        public ProdutoChamadaServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ProdutoChamadaServiceTests");
        }

        [Fact]
        public void TestaMetodoGet()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Get");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();
            var identifier = Guid.NewGuid();

            mockProdutoChamadaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var planoBasico = new PlanoBasico("Produto De Teste")
                    {
                        Id = identifier
                    };

                    return planoBasico;
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.Get(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O produtoChamada deve ter um Id válido");
            result.Id.Should().Be(identifier);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O produtoChamada deve ter o nome 'Produto De Teste'");
            result.Nome.Should().Be("Produto De Teste");
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo All");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.All())
                .Returns(() =>
                {
                    var produtos = new ProdutoChamada[]
                    {
                        new PlanoBasico("Plano Básico"),
                        new FaleMais("FaleMais 30", 30),
                        new FaleMais("FaleMais 60", 60),
                        new FaleMais("FaleMais 120", 120)
                    };

                    return produtos;
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.All();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 4 itens");
            result.Count().Should().Be(4);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 1 produtoChamada com o nome 'FaleMais 120'");
            result.Count(o => o.Nome.Equals("FaleMais 120", StringComparison.CurrentCultureIgnoreCase)).Should().Be(1);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFind()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Find");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<ProdutoChamada, bool>>>()))
                .Returns(() =>
                {
                    var produtos = new ProdutoChamada[]
                    {
                        new PlanoBasico("Plano Básico"),
                        new FaleMais("FaleMais 30", 30),
                        new FaleMais("FaleMais 60", 60),
                        new FaleMais("FaleMais 120", 120)
                    };

                    return produtos.Single(o => o.Nome.Equals("FaleMais 120", StringComparison.CurrentCultureIgnoreCase));
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.Find(o => o.Nome.Equals("FaleMais 120", StringComparison.CurrentCultureIgnoreCase));

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 1 produtoChamada com o nome 'FaleMais 120'");
            result.Nome.Equals("FaleMais 120", StringComparison.CurrentCultureIgnoreCase).Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFindAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo FindAll retornando somente produtos do tipo FaleMais");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.FindAll(It.IsAny<Expression<Func<ProdutoChamada, bool>>>()))
                .Returns(() =>
                {
                    var produtos = new ProdutoChamada[]
                    {
                        new PlanoBasico("Plano Básico"),
                        new FaleMais("FaleMais 30", 30),
                        new FaleMais("FaleMais 60", 60),
                        new FaleMais("FaleMais 120", 120)
                    };

                    return produtos.Where(o => o.GetType() == typeof(FaleMais)).ToArray();
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.FindAll(o => o.GetType() == typeof(FaleMais));

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 3 produtos FaleMais de uma lista de 4");
            result.Count().Should().Be(3);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteTarifa()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um produto");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            var planoBasico = new PlanoBasico("Produto De Teste")
            {
                Id = Guid.NewGuid()
            };

            mockProdutoChamadaRepository
                .Setup(x => x.Delete(It.IsAny<ProdutoChamada>()))
                .Returns(true);

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.Delete(planoBasico);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteClientePeloIdentifier()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um identifier de um produto");

            var identifier = Guid.NewGuid();
            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(true);

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.Delete(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSave()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Save do Produto");

            var planoBasico = new PlanoBasico("Produto De Teste");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.Save(It.IsAny<ProdutoChamada>()))
                .Returns(() =>
                {
                    planoBasico.Id = Guid.NewGuid();
                    return planoBasico;
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.Save(planoBasico);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar um Produto com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoBuscaProdutoChamadaAtivo()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo BuscaProdutoChamadaAtivo");

            var produto = new FaleMais("FaleMais 60", 60);
            var cliente = new Cliente("Peter Parker");

            var mockProdutoChamadaRepository = new Mock<IProdutoChamadaRepository>();

            mockProdutoChamadaRepository
                .Setup(x => x.BuscaProdutoChamadaAtivo(It.IsAny<Cliente>()))
                .Returns(() =>
                {
                    return produto;
                });

            var produtoChamadaService = new ProdutoChamadaService(mockProdutoChamadaRepository.Object);
            var result = produtoChamadaService.BuscaProdutoChamadaAtivo(cliente);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar um ProdutoChamada do cliente que esteja ativo");
            result.Nome.Should().Be("FaleMais 60");
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
