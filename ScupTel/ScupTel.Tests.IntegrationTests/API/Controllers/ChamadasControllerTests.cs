using FluentAssertions;
using Newtonsoft.Json;
using ScupTel.API.DataTransferObject;
using ScupTel.Tests.IntegrationsTests.Configuration;
using ScupTel.Domain.Core.Helpers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using Xunit.Abstractions;
using System.Net.Http;

namespace ScupTel.Tests.IntegrationsTests.API.Controllers
{
    public class ChamadasControllerTests : BaseIntegrationTest
    {
        private const string BaseUrl = "/Chamadas";

        public ChamadasControllerTests(BaseTestFixture fixture) 
            : base(fixture)
        {
            TestConsoleWriterHelper.PrintTestClass("ChamadasControllerTests");
        }

        // fazer um teste para cada simulacao do quadro
        [Fact]
        public async Task SimulacaoChamadaProdutoFaleMais30()
        {
            TestConsoleWriterHelper.PrintTestName("SimulacaoChamadaProdutoFaleMais30");

            var produto = await TestDbContext.ProdutosChamada.SingleAsync(o => o.Nome.Equals("FaleMais 30", StringComparison.CurrentCultureIgnoreCase));
            string queryString = @"/SimulacaoProdutoFaleMais?dddOrigem={0}&dddDestino={1}&tempoChamada={2}&produtoFaleMaisId={3}";
            string uri = string.Concat(BaseUrl, string.Format(queryString, 11, 16, 20, produto.Id));

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SimulacaoChamadaDto>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("Origem deve ser 11");
            data.Origem.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Destino deve ser 16");
            data.Destino.Should().Be(16);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Tempo deve ser 20");
            data.Tempo.Should().Be(20);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("PlanoFaleMais deve ser 'FaleMais 30'");
            data.PlanoFaleMais.Should().Be("FaleMais 30");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("ComFaleMais deve ser '0.00'");
            data.ComFaleMais.HasValue.Should().Be(true);
            data.ComFaleMais.Value.Should().Be(0.00m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("SemFaleMais deve ser '38.00'");
            data.SemFaleMais.HasValue.Should().Be(true);
            data.SemFaleMais.Value.Should().Be(38.00m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public async Task SimulacaoChamadaProdutoFaleMais60()
        {
            TestConsoleWriterHelper.PrintTestName("SimulacaoChamadaProdutoFaleMais60");

            var produto = await TestDbContext.ProdutosChamada.SingleAsync(o => o.Nome.Equals("FaleMais 60", StringComparison.CurrentCultureIgnoreCase));
            string queryString = @"/SimulacaoProdutoFaleMais?dddOrigem={0}&dddDestino={1}&tempoChamada={2}&produtoFaleMaisId={3}";
            string uri = string.Concat(BaseUrl, string.Format(queryString, 11, 17, 80, produto.Id));

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SimulacaoChamadaDto>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("Origem deve ser 11");
            data.Origem.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Destino deve ser 17");
            data.Destino.Should().Be(17);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Tempo deve ser 80");
            data.Tempo.Should().Be(80);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("PlanoFaleMais deve ser 'FaleMais 60'");
            data.PlanoFaleMais.Should().Be("FaleMais 60");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("ComFaleMais deve ser '37.40'");
            data.ComFaleMais.HasValue.Should().Be(true);
            data.ComFaleMais.Value.Should().Be(37.40m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("SemFaleMais deve ser '136.00'");
            data.SemFaleMais.HasValue.Should().Be(true);
            data.SemFaleMais.Value.Should().Be(136.00m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public async Task SimulacaoChamadaProdutoFaleMais120()
        {
            TestConsoleWriterHelper.PrintTestName("SimulacaoChamadaProdutoFaleMais120");

            var produto = await TestDbContext.ProdutosChamada.SingleAsync(o => o.Nome.Equals("FaleMais 120", StringComparison.CurrentCultureIgnoreCase));
            string queryString = @"/SimulacaoProdutoFaleMais?dddOrigem={0}&dddDestino={1}&tempoChamada={2}&produtoFaleMaisId={3}";
            string uri = string.Concat(BaseUrl, string.Format(queryString, 18, 11, 200, produto.Id));

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SimulacaoChamadaDto>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("Origem deve ser 18");
            data.Origem.Should().Be(18);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Destino deve ser 11");
            data.Destino.Should().Be(11);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Tempo deve ser 200");
            data.Tempo.Should().Be(200);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("PlanoFaleMais deve ser 'FaleMais 120'");
            data.PlanoFaleMais.Should().Be("FaleMais 120");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("ComFaleMais deve ser '167.20'");
            data.ComFaleMais.HasValue.Should().Be(true);
            data.ComFaleMais.Value.Should().Be(167.20m);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("SemFaleMais deve ser '380.00'");
            data.SemFaleMais.HasValue.Should().Be(true);
            data.SemFaleMais.Value.Should().Be(380.00m);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public async Task SimulacaoChamadaProdutoFaleMais30DistanciaNaoDisponivel()
        {
            TestConsoleWriterHelper.PrintTestName("SimulacaoChamadaProdutoFaleMais30NaoDisponivel");

            var produto = await TestDbContext.ProdutosChamada.SingleAsync(o => o.Nome.Equals("FaleMais 30", StringComparison.CurrentCultureIgnoreCase));
            string queryString = @"/SimulacaoProdutoFaleMais?dddOrigem={0}&dddDestino={1}&tempoChamada={2}&produtoFaleMaisId={3}";
            string uri = string.Concat(BaseUrl, string.Format(queryString, 18, 17, 100, produto.Id));

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SimulacaoChamadaDto>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("Origem deve ser 18");
            data.Origem.Should().Be(18);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Destino deve ser 17");
            data.Destino.Should().Be(17);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("Tempo deve ser 100");
            data.Tempo.Should().Be(100);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("PlanoFaleMais deve ser 'FaleMais 30'");
            data.PlanoFaleMais.Should().Be("FaleMais 30");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("ComFaleMais deve ser 'nulo'");
            data.ComFaleMais.Should().Be(null);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("SemFaleMais deve ser 'nulo'");
            data.SemFaleMais.Should().Be(null);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public async Task SimulacaoChamadaRetorna404()
        {
            TestConsoleWriterHelper.PrintTestName("SimulacaoChamadaRetorna404 quando produto não é do tipo FaleMais");

            var produto = await TestDbContext.ProdutosChamada.SingleAsync(o => o.Nome.Equals("Plano Básico", StringComparison.CurrentCultureIgnoreCase));
            string queryString = @"/SimulacaoProdutoFaleMais?dddOrigem={0}&dddDestino={1}&tempoChamada={2}&produtoFaleMaisId={3}";
            string uri = string.Concat(BaseUrl, string.Format(queryString, 18, 17, 100, produto.Id));

            var response = await HttpClient.GetAsync(uri);
            Action result = () => response.EnsureSuccessStatusCode();
            
            TestConsoleWriterHelper.PrintAssertResult("Deve retornar erro 404 NotFound");
            result.ShouldThrow<HttpRequestException>();
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
