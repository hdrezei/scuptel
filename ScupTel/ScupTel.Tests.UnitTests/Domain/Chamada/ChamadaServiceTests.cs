using FluentAssertions;
using Moq;
using ScupTel.Domain.Atendimento;
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
    public class ChamadaServiceTests
    {
        public ChamadaServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadaServiceTests");
        }

        [Fact]
        public void TestaMetodoGet()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Get");

            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            var identifier = Guid.NewGuid();
            var inicioChamada = DateTime.Now;
            var duracao = new TimeSpan(0, 20, 0);

            mockChamadaBaseRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16);

                    var chamada = new ChamadaRealizada(cobertura, inicioChamada, duracao)
                    {
                        Id = identifier
                    };

                    return chamada;
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.Get(identifier);

            TestConsoleWriterHelper.PrintAssertResult("A chamada deve ter um Id válido");
            result.Id.Should().Be(identifier);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A chamada deve ter o inicio igual a '" + inicioChamada + "'");
            result.Inicio.Should().Be(inicioChamada);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A chamada deve ter o fim igual a '" + inicioChamada.AddMinutes(duracao.TotalMinutes) + "'");
            result.Fim.Should().Be(inicioChamada.AddMinutes(duracao.TotalMinutes));
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo All");

            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockChamadaBaseRepository
                .Setup(x => x.All())
                .Returns(() =>
                {
                    var cliente = new Cliente("Tony Stark");
                    var telefone = new Telefone(985207410, 11, 55, cliente);
                    var cobertura = new Cobertura(11, 16);
                    var chamadas = new ChamadaBase[]
                    {
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 80, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 50, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 15, 0))
                    };

                    return chamadas;
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.All();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 5 itens");
            result.Count().Should().Be(5);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFind()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Find");

            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockChamadaBaseRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<ChamadaBase, bool>>>()))
                .Returns(() =>
                {
                    var cliente = new Cliente("Tony Stark");
                    var telefone = new Telefone(985207410, 11, 55, cliente);
                    var cobertura = new Cobertura(11, 16);
                    var chamadas = new ChamadaBase[]
                    {
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 80, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 23235656, cobertura, DateTime.Now, new TimeSpan(0, 50, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 15, 0))
                    };

                    return chamadas.Single(o => o.GetType() == typeof(ChamadaCliente) && ((ChamadaCliente)o).NumeroTelefoneDiscado == 23235656);
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.Find(o => o.GetType() == typeof(ChamadaCliente) && ((ChamadaCliente)o).NumeroTelefoneDiscado == 23235656);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma chamada com duracao de '50' minutos");
            result.Duracao.TotalMinutes.Should().Be(50);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFindAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo FindAll retornando somente cobertura com dddOrigem 11");

            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockChamadaBaseRepository
                .Setup(x => x.FindAll(It.IsAny<Expression<Func<ChamadaBase, bool>>>()))
                .Returns(() =>
                {
                    var cliente = new Cliente("Tony Stark");
                    var telefone = new Telefone(985207410, 11, 55, cliente);
                    var cobertura = new Cobertura(11, 16);
                    var chamadas = new ChamadaBase[]
                    {
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 80, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 20, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 23235656, cobertura, DateTime.Now, new TimeSpan(0, 50, 0)),
                        new ChamadaCliente(cliente, telefone.Numero, 963635252, cobertura, DateTime.Now, new TimeSpan(0, 15, 0))
                    };

                    return chamadas.Where(o => o.GetType() == typeof(ChamadaCliente) && ((ChamadaCliente)o).Cliente.Nome.Equals("Tony Stark", StringComparison.CurrentCultureIgnoreCase)).ToArray();
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.FindAll(o => o.GetType() == typeof(ChamadaCliente) && ((ChamadaCliente)o).Cliente.Nome.Equals("Tony Stark", StringComparison.CurrentCultureIgnoreCase));

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 3 chamadas do cliente especificado");
            result.Count().Should().Be(3);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteTarifa()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo uma chamada");

            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(985207410, 11, 55, cliente);
            var cobertura = new Cobertura(11, 16);
            var chamadaCliente = new ChamadaCliente(cliente, telefone.Numero, 23235656, cobertura, DateTime.Now, new TimeSpan(0, 50, 0))
            {
                Id = Guid.NewGuid()
            };

            mockChamadaBaseRepository
                .Setup(x => x.Delete(It.IsAny<ChamadaCliente>()))
                .Returns(true);

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.Delete(chamadaCliente);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteClientePeloIdentifier()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um identifier de uma chamada");

            var identifier = Guid.NewGuid();
            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockChamadaBaseRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(true);

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.Delete(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSave()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Save da Chamada");

            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(985207410, 11, 55, cliente);
            var cobertura = new Cobertura(11, 16);
            var chamadaCliente = new ChamadaCliente(cliente, telefone.Numero, 23235656, cobertura, DateTime.Now, new TimeSpan(0, 50, 0));

            var identifier = Guid.NewGuid();
            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockChamadaBaseRepository
                .Setup(x => x.Save(It.IsAny<ChamadaCliente>()))
                .Returns(() =>
                {
                    chamadaCliente.Id = Guid.NewGuid();
                    return chamadaCliente;
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.Save(chamadaCliente);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma Cobertura com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoRegistrarChamada()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo RegistrarChamada");

            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(985207410, 11, 55, cliente);
            var numeroDiscado = 23235656;
            var dddOrigem = 11;
            var dddDestino = 16;
            var cobertura = new Cobertura(dddOrigem, dddDestino);
            var inicioChamada = DateTime.Now;
            var duracao = new TimeSpan(0, 50, 0);
            var chamadaCliente = new ChamadaCliente(cliente, telefone.Numero, numeroDiscado, cobertura, inicioChamada, duracao);

            var identifier = Guid.NewGuid();
            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() => { return cobertura; });

            mockChamadaBaseRepository
                .Setup(x => x.Save(It.IsAny<ChamadaCliente>()))
                .Returns(() =>
                {
                    chamadaCliente.Id = Guid.NewGuid();
                    return chamadaCliente;
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.RegistrarChamada(telefone, numeroDiscado, cobertura.DddOrigem, cobertura.DddDestino, inicioChamada, duracao);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCliente com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoCalcularChamada()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo CalcularChamada");

            var produto = new FaleMais("FaleMais 20", 20);
            var cliente = new Cliente("Tony Stark");
            var telefone = new Telefone(985207410, 11, 55, cliente);
            var numeroDiscado = 23235656;
            var cobertura = new Cobertura(11, 16);
            var inicioChamada = DateTime.Now;
            var duracao = new TimeSpan(0, 50, 0);
            var chamadaCliente = new ChamadaCliente(cliente, telefone.Numero, numeroDiscado, cobertura, inicioChamada, duracao);
            var chamadaTarifada = new ChamadaTarifada(chamadaCliente);

            var identifier = Guid.NewGuid();
            var mockChamadaBaseRepository = new Mock<IChamadaBaseRepository>();
            var mockTarifaChamadaService = new Mock<ITarifaChamadaService>();
            var mockCoberturaService = new Mock<ICoberturaService>();
            var mockProdutoChamadaService = new Mock<IProdutoChamadaService>();
            var mockChamadaTarifadaRepository = new Mock<IChamadaTarifadaRepository>();
            var mockClienteService = new Mock<IClienteService>();

            mockCoberturaService
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() => { return cobertura; });

            mockTarifaChamadaService
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<Cobertura>()))
                .Returns(() =>
                {
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia, dataFinalVigencia, 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", cobertura, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", cobertura, dataInicioVigencia, dataFinalVigencia, 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", cobertura, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m)
                    };

                    return tarifasChamada;
                });

            mockProdutoChamadaService
                .Setup(x => x.BuscaProdutoChamadaAtivo(It.IsAny<Cliente>()))
                .Returns(() => { return produto; });

            mockChamadaTarifadaRepository
                .Setup(x => x.Save(It.IsAny<ChamadaTarifada>()))
                .Returns(() =>
                {
                    chamadaTarifada.Id = Guid.NewGuid();
                    return chamadaTarifada;
                });

            var chamadaService = new ChamadaService(
                mockTarifaChamadaService.Object,
                mockCoberturaService.Object,
                mockProdutoChamadaService.Object,
                mockChamadaBaseRepository.Object,
                mockChamadaTarifadaRepository.Object,
                mockClienteService.Object);

            var result = chamadaService.CalcularChamada(cliente, telefone.Numero, numeroDiscado, cobertura.DddOrigem, cobertura.DddDestino, inicioChamada, duracao);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com uma ChamadaTarifada com identifier valido");
            result.ChamadaTarifada.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um ProdutoChamada com o nome 'FaleMais 20'");
            result.Produto.Nome.Should().Be("FaleMais 20");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma ChamadaCalculada com um valor igual '742.5'");
            result.Valor.Value.Should().Be(742.5m);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
