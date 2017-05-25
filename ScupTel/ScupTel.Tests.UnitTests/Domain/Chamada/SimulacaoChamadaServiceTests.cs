using FluentAssertions;
using Moq;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Chamada
{
    public class SimulacaoChamadaServiceTests
    {
        public SimulacaoChamadaServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("SimulacaoChamadaServiceTests");
        }

        [Fact]
        public void TestaMetodoInternoSimulacaoChamadaSemCobertura()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo SimulacaoChamada sem cobertura");

            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() => { return null; });

            var simulacaoChamadaService = new SimulacaoChamadaService(
                mockCoberturaService.Object,
                mockTarifaChamadaService.Object,
                mockProdutoChamadaService.Object);

            var produtoChamada = new PlanoBasico("Plano Básico")
            {
                Id = Guid.NewGuid()
            };

            var result = simulacaoChamadaService.SimulacaoChamada(11, 18, 23, produtoChamada);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com uma ChamadaTarifada nula");
            result.ChamadaTarifada.Should().BeNull();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um ProdutoChamada com o nome 'Plano Básico'");
            result.Produto.Nome.Should().Be("Plano Básico");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um valor nulo");
            result.Valor.Should().BeNull();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoInternoSimulacaoChamada()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo SimulacaoChamada");

            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 18)
                    {
                        Id = Guid.NewGuid()
                    };

                    return cobertura;
                });

            mockTarifaChamadaService
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<Cobertura>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 17);
                    var coberturaRegiaoDiferente = new Cobertura(11, 18);
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia, dataFinalVigencia, 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m),
                    };

                    return tarifasChamada.Where(o => o.Cobertura.DddOrigem == 11 && o.Cobertura.DddDestino == 18).ToArray();
                });

            var simulacaoChamadaService = new SimulacaoChamadaService(
                mockCoberturaService.Object,
                mockTarifaChamadaService.Object,
                mockProdutoChamadaService.Object);

            var produtoChamada = new PlanoBasico("Plano Básico")
            {
                Id = Guid.NewGuid()
            };

            var result = simulacaoChamadaService.SimulacaoChamada(11, 18, 23, produtoChamada);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com uma ChamadaTarifada com identifier vazio por ser uma simulacao");
            result.ChamadaTarifada.Id.Should().BeEmpty();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um ProdutoChamada com o nome 'Plano Básico'");
            result.Produto.Nome.Should().Be("Plano Básico");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um valor igual '293.25'");
            result.Valor.Value.Should().Be(293.25m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSimulacaoPlanoBasico()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo SimulacaoPlanoBasico");

            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 18)
                    {
                        Id = Guid.NewGuid()
                    };

                    return cobertura;
                });

            mockTarifaChamadaService
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<Cobertura>()))
                .Returns(() => 
                {
                    var cobertura = new Cobertura(11, 17);
                    var coberturaRegiaoDiferente = new Cobertura(11, 18);
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia, dataFinalVigencia, 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m),
                    };

                    return tarifasChamada.Where(o => o.Cobertura.DddOrigem == 11 && o.Cobertura.DddDestino == 18).ToArray();
                });

            mockProdutoChamadaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<ProdutoChamada, bool>>>()))
                .Returns(() =>
                {
                    var produtoChamada = new PlanoBasico("Plano Básico")
                    {
                        Id = Guid.NewGuid()
                    };

                    return produtoChamada;
                });

            var simulacaoChamadaService = new SimulacaoChamadaService(
                mockCoberturaService.Object, 
                mockTarifaChamadaService.Object, 
                mockProdutoChamadaService.Object);

            var result = simulacaoChamadaService.SimulacaoPlanoBasico(11, 18, 55);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com uma ChamadaTarifada com identifier vazio por ser uma simulacao");
            result.ChamadaTarifada.Id.Should().BeEmpty();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um ProdutoChamada com o nome 'Plano Básico'");
            result.Produto.Nome.Should().Be("Plano Básico");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um valor igual '701.25'");
            result.Valor.Value.Should().Be(701.25m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSimulacaoFaleMaisRetornaArgumentException()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo SimulacaoFaleMais retornar ArgumentException quando o produto não é do tipo FaleMais");

            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            
            mockProdutoChamadaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<ProdutoChamada, bool>>>()))
                .Returns(() => { return null; });

            var simulacaoChamadaService = new SimulacaoChamadaService(
                mockCoberturaService.Object,
                mockTarifaChamadaService.Object,
                mockProdutoChamadaService.Object);

            var identifier = Guid.NewGuid();
            Action result = () => simulacaoChamadaService.SimulacaoFaleMais(11, 16, 55, identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ArgumentException com o identifier na mensagem");
            result.ShouldThrow<ArgumentException>().And.Message.Should().Contain(identifier.ToString());
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSimulacaoFaleMais()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo SimulacaoFaleMais");

            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16)
                    {
                        Id = Guid.NewGuid()
                    };

                    return cobertura;
                });

            mockTarifaChamadaService
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<Cobertura>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16);
                    var coberturaRegiaoDiferente = new Cobertura(11, 17);
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia, dataFinalVigencia, 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m),
                    };

                    return tarifasChamada.Where(o => o.Cobertura.DddOrigem == 11 && o.Cobertura.DddDestino == 16).ToArray();
                });

            mockProdutoChamadaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<ProdutoChamada, bool>>>()))
                .Returns(() =>
                {
                    var produtoChamada = new FaleMais("FaleMais 40", 40)
                    {
                        Id = Guid.NewGuid()
                    };

                    return produtoChamada;
                });

            var simulacaoChamadaService = new SimulacaoChamadaService(
                mockCoberturaService.Object,
                mockTarifaChamadaService.Object,
                mockProdutoChamadaService.Object);

            var result = simulacaoChamadaService.SimulacaoFaleMais(11, 16, 55, Guid.NewGuid());

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com uma ChamadaTarifada com identifier vazio por ser uma simulacao");
            result.ChamadaTarifada.Id.Should().BeEmpty();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um ProdutoChamada com o nome 'FaleMais 40'");
            result.Produto.Nome.Should().Be("FaleMais 40");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um valor igual '160.88'");
            result.Valor.Value.Should().Be(160.88m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}