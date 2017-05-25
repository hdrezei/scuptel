using FluentAssertions;
using Moq;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace ScupTel.Tests.UnitTests.Domain.Localidade
{
    public class CoberturaServiceTests
    {
        public CoberturaServiceTests()
        {
            TestConsoleWriterHelper.PrintTestClass("CoberturaServiceTests");
        }

        [Fact]
        public void TestaMetodoGet()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Get");

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();
            var identifier = Guid.NewGuid();

            mockCoberturaRepository
                .Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(() =>
                {
                    var cobertura = new Cobertura(11, 16)
                    {
                        Id = identifier
                    };

                    return cobertura;
                });

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.Get(identifier);

            TestConsoleWriterHelper.PrintAssertResult("A cobertura deve ter um Id válido");
            result.Id.Should().Be(identifier);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A cobertura deve ter o dddOrigem igual a '11'");
            result.DddOrigem.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();
            
            TestConsoleWriterHelper.PrintAssertResult("A cobertura deve ter o dddDestino igual a '16'");
            result.DddDestino.Should().Be(16);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo All");

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            mockCoberturaRepository
                .Setup(x => x.All())
                .Returns(() =>
                {
                    var coberturas = new Cobertura[]
                    {
                        new Cobertura(11, 16),
                        new Cobertura(16, 11),
                        new Cobertura(11, 17),
                        new Cobertura(17, 11),
                        new Cobertura(11, 18),
                        new Cobertura(18, 11)
                    };

                    return coberturas;
                });

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.All();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter 6 itens");
            result.Count().Should().Be(6);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A lista deve conter uma cobertura com dddOrigem '11' e dddDestino '18'");
            result.Count(o => o.DddOrigem == 11 && o.DddDestino == 18).Should().Be(1);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFind()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Find");

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            mockCoberturaRepository
                .Setup(x => x.Find(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() =>
                {
                    var coberturas = new Cobertura[]
                    {
                        new Cobertura(11, 16),
                        new Cobertura(16, 11),
                        new Cobertura(11, 17),
                        new Cobertura(17, 11),
                        new Cobertura(11, 18),
                        new Cobertura(18, 11)
                    };

                    return coberturas.Single(o => o.DddOrigem == 16 && o.DddDestino == 11);
                });

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.Find(o => o.DddOrigem == 16 && o.DddDestino == 11);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma cobertura com o dddOrigem '16'");
            result.DddOrigem.Should().Be(16);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma cobertura com o dddOrigem '11'");
            result.DddDestino.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoFindAll()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo FindAll retornando somente cobertura com dddOrigem 11");

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            mockCoberturaRepository
                .Setup(x => x.FindAll(It.IsAny<Expression<Func<Cobertura, bool>>>()))
                .Returns(() =>
                {
                    var coberturas = new Cobertura[]
                    {
                        new Cobertura(11, 16),
                        new Cobertura(16, 11),
                        new Cobertura(11, 17),
                        new Cobertura(17, 11),
                        new Cobertura(11, 18),
                        new Cobertura(18, 11)
                    };

                    return coberturas.Where(o => o.DddOrigem == 11).ToArray();
                });

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.FindAll(o => o.DddOrigem == 11);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar 3 coberturas de uma lista de 6");
            result.Count().Should().Be(3);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteTarifa()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo uma cobertura");

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            var cobertura = new Cobertura(11, 16)
            {
                Id = Guid.NewGuid()
            };

            mockCoberturaRepository
                .Setup(x => x.Delete(It.IsAny<Cobertura>()))
                .Returns(true);

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.Delete(cobertura);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoDeleteClientePeloIdentifier()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Delete recebendo um identifier de uma cobertura");

            var identifier = Guid.NewGuid();
            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            mockCoberturaRepository
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(true);

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.Delete(identifier);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar que aa deleção foi bem sucedida");
            result.Should().BeTrue();
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public void TestaMetodoSave()
        {
            TestConsoleWriterHelper.PrintTestName("Testa o metodo Save da Cobertura");

            var cobertura = new Cobertura(11, 16);

            var mockCoberturaRepository = new Mock<ICoberturaRepository>();

            mockCoberturaRepository
                .Setup(x => x.Save(It.IsAny<Cobertura>()))
                .Returns(() =>
                {
                    cobertura.Id = Guid.NewGuid();
                    return cobertura;
                });

            var coberturaService = new CoberturaService(mockCoberturaRepository.Object);
            var result = coberturaService.Save(cobertura);

            TestConsoleWriterHelper.PrintAssertResult("O metodo deve retornar uma Cobertura com um numero de identifier");
            result.Id.Should().NotBeEmpty();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
