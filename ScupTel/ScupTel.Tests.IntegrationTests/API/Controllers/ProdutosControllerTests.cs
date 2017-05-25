using FluentAssertions;
using Newtonsoft.Json;
using ScupTel.API.DataTransferObject;
using ScupTel.Tests.IntegrationsTests.Configuration;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using ScupTel.Domain.Core.Helpers;
using System.Net.Http;
using System;

namespace ScupTel.Tests.IntegrationsTests.API.Controllers
{
    public class ProdutosControllerTests : BaseIntegrationTest
    {
        private const string BaseUrl = "/Produtos";

        public ProdutosControllerTests(BaseTestFixture fixture)
            : base(fixture)
        {

            TestConsoleWriterHelper.PrintTestClass("ProdutosControllerTests");
        }

        [Fact]
        public async Task DeveRetornarUmaListaDeProdutos()
        {
            TestConsoleWriterHelper.PrintTestName("Deve retornar uma lista de produtos");

            string uri = string.Concat(BaseUrl);

            var response = await HttpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ProdutoChamadaDto>>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("A Lista deve conter 4 items");
            data.Count.Should().Be(4);
            TestConsoleWriterHelper.TestPassed();
        }

        [Fact]
        public async Task AdicionaProduto()
        {
            TestConsoleWriterHelper.PrintTestName("Adicionar um novo produto");

            // testa a adição do protudo
            string uri = string.Concat(BaseUrl);

            var produtoFaleMais = new ProdutoChamadaFaleMaisDto("FaleMaisTest", 50);

            StringContent content = new StringContent(JsonConvert.SerializeObject(produtoFaleMais), System.Text.Encoding.UTF8, "application/json");
            var responsePost = await HttpClient.PostAsync(uri, content);
            responsePost.EnsureSuccessStatusCode();

            var responseStringPost = await responsePost.Content.ReadAsStringAsync();
            var produtoId = JsonConvert.DeserializeObject<Guid>(responseStringPost);

            TestConsoleWriterHelper.PrintAssertResult("Deve retornar um Identifier");
            produtoId.Should().NotBe(Guid.Empty);
            TestConsoleWriterHelper.TestPassed();

            var responseGet = await HttpClient.GetAsync(string.Concat(uri, "/", produtoId));
            responsePost.EnsureSuccessStatusCode();

            var responseStringGet = await responseGet.Content.ReadAsStringAsync();
            var produto = JsonConvert.DeserializeObject<ProdutoChamadaFaleMaisDto>(responseStringGet);

            TestConsoleWriterHelper.PrintAssertResult("O produto adicionado deve ter um Identifier");
            produto.Identifier.Should().NotBe(Guid.Empty);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O produto adicionado deve ter o nome FaleMaisTest");
            produto.Nome.Should().Be("FaleMaisTest");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O protudo adicionado deve ter um valor de MinutosFranquia igual a 50");
            produto.MinutosFranquia.Should().Be(50);
            TestConsoleWriterHelper.TestPassed();

            // testa consulta de produtos
            var responseGetList = await HttpClient.GetAsync(uri);
            responseGet.EnsureSuccessStatusCode();

            var responseStringGetList = await responseGetList.Content.ReadAsStringAsync();
            var listaProdutos = JsonConvert.DeserializeObject<List<ProdutoChamadaDto>>(responseStringGetList);

            TestConsoleWriterHelper.PrintAssertResult("A Lista deve conter 5 items");
            listaProdutos.Count.Should().Be(5);
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A Lista deve conter um produto com nome FaleMaisTest");
            listaProdutos.Any(o => o.Nome == "FaleMaisTest").Should().Be(true);
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
