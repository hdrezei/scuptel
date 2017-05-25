using FluentAssertions;
using Moq;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Tarifa
{
    public class TarifaChamadaServiceTests
    {
        public TarifaChamadaServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("TarifaChamadaServiceTests");
        }

        [Fact]
        public void TestaMetodoGet()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Get");

            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();
            var identifier = Guid.NewGuid();

            mockTarifaChamadaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16);
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var longaDistanciaNacional = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m)
                    {
                        Id = identifier
                    };

                    return longaDistanciaNacional;
                });

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.Get(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O tarifaChamada deve ter um Id válido");
            result.Id.Should().Be(identifier);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O tarifaChamada deve ter o nome 'Tarifa de Teste LongaDistanciaNacional'");
            result.Nome.Should().Be("Tarifa de Teste LongaDistanciaNacional");
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo All");

            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.All())
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16);
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

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.All();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 6 itens");
            result.Count().Should().Be(6);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 1 cliente com o nome 'Tarifa de Teste LongaDistanciaNacional 5'");
            result.Count(o => o.Nome.Equals("Tarifa de Teste LongaDistanciaNacional 5", StringComparison.CurrentCultureIgnoreCase)).Should().Be(1);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFind()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Find");

            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<TarifaChamada, bool>>>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16);
                    var dataInicioVigencia = DateTime.Now.AddMonths(-1);
                    var dataFinalVigencia = DateTime.Now.AddMonths(1);

                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia, dataFinalVigencia, 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", cobertura, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", cobertura, dataInicioVigencia, dataFinalVigencia, 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", cobertura, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m),
                    };

                    return tarifasChamada.Single(o => o.Nome.Equals("Tarifa de Teste LongaDistanciaNacional 4", StringComparison.CurrentCultureIgnoreCase));
                });

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.Find(o => o.Nome.Equals("Tarifa de Teste LongaDistanciaNacional 4", StringComparison.CurrentCultureIgnoreCase));

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 1 cliente com o nome 'Tarifa de Teste LongaDistanciaNacional 4'");
            result.Nome.Equals("Tarifa de Teste LongaDistanciaNacional 4", StringComparison.CurrentCultureIgnoreCase).Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFindAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo FindAll retornando somente tarifas vigentes");

            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();

            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var chamadaRealizada = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, 20, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaRealizada);

            mockTarifaChamadaRepository
                .Setup(x => x.FindAll(It.IsAny<Expression<Func<TarifaChamada, bool>>>()))
                .Returns(() =>
                {
                    var tarifasChamada = new LongaDistanciaNacional[]
                    {
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 1", cobertura, dataInicioVigencia, dataFinalVigencia, 1.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 2", cobertura, dataInicioVigencia.AddMonths(-2), dataFinalVigencia.AddMonths(-2), 2.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 3", cobertura, dataInicioVigencia, dataFinalVigencia, 3.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 4", cobertura, dataInicioVigencia.AddMonths(-2), dataFinalVigencia.AddMonths(-2), 4.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 5", cobertura, dataInicioVigencia, dataFinalVigencia, 5.25m),
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m)
                    };

                    return tarifasChamada.Where(o => o.TarifaVigente(chamadaTarifada) == true).ToArray();
                });

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.FindAll(o => o.TarifaVigente(chamadaTarifada) == true);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 4 tarifas de uma lista de 6");
            result.Count().Should().Be(4);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteTarifa()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo uma tarifaChamada");

            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m);

            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.Delete(It.IsAny<TarifaChamada>()))
                .Returns(true);

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.Delete(tarifaChamada);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteClientePeloIdentifier()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um identifier de uma tarifaChamada");

            var identifier = Guid.NewGuid();
            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(true);

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.Delete(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSave()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Save TarifaChamada");

            var cobertura = new Cobertura(11, 16);
            var dataInicioVigencia = DateTime.Now.AddMonths(-1);
            var dataFinalVigencia = DateTime.Now.AddMonths(1);

            var tarifaChamada = new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", cobertura, dataInicioVigencia, dataFinalVigencia, 6.25m);

            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();
            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.Save(It.IsAny<TarifaChamada>()))
                .Returns(() =>
                {
                    tarifaChamada.Id = Guid.NewGuid();
                    return tarifaChamada;
                });

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.Save(tarifaChamada);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar TarifaChamada com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoBuscarTarifasChamadaPorCobertura()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo BuscarTarifasChamadaPorCobertura");

            var cobertura = new Cobertura(11, 16);

            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();
            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
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

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.BuscarTarifasChamadaPorCobertura(cobertura);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma lista de 3 tarifas");
            result.Count().Should().Be(3);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoBuscarTarifasChamadaPorDddOrigemEDddDestino()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo BuscarTarifasChamadaPorCobertura");
            
            var mockResumoCalculoTarifaChamadaRepository = new Mock<IResumoCalculoTarifaChamadaRepository>();
            var mockTarifaChamadaRepository = new Mock<ITarifaChamadaRepository>();

            mockTarifaChamadaRepository
                .Setup(x => x.BuscarTarifasChamadaPorCobertura(It.IsAny<int>(), It.IsAny<int>()))
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
                        new LongaDistanciaNacional("Tarifa de Teste LongaDistanciaNacional 6", coberturaRegiaoDiferente, dataInicioVigencia, dataFinalVigencia, 6.25m),
                    };

                    return tarifasChamada.Where(o => o.Cobertura.DddOrigem == 11 && o.Cobertura.DddDestino == 17).ToArray();
                });

            var tarifaChamadaService = new TarifaChamadaService(mockTarifaChamadaRepository.Object, mockResumoCalculoTarifaChamadaRepository.Object);
            var result = tarifaChamadaService.BuscarTarifasChamadaPorCobertura(11, 17);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma lista de 4 tarifas");
            result.Count().Should().Be(4);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
