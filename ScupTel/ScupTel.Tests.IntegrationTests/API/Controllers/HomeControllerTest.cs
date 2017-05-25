using FluentAssertions;
using Newtonsoft.Json;
using ScupTel.API.DataTransferObject;
using ScupTel.Domain.Core.Helpers;
using ScupTel.Tests.IntegrationsTests.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ScupTel.Tests.IntegrationsTests.API.Controllers
{
    public class HomeControllerTest : BaseIntegrationTest
    {
        private const string BaseUrl = "/Home";

        public HomeControllerTest(BaseTestFixture fixture) 
            : base(fixture)
        {
            TestConsoleWriterHelper.PrintTestClass("HomeControllerTests");
        }

        [Fact]
        public async Task DeveRetornarStatusServer()
        {
            TestConsoleWriterHelper.PrintTestName("DeveRetornarStatusServer");

            var response = await HttpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ApiStatusDto>(responseString);

            TestConsoleWriterHelper.PrintAssertResult("O ServiceName deve ser 'ScupTel.API'");
            data.ServiceName.Should().Be("ScupTel.API");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("O Status deve ser 'Running'");
            data.Status.Should().Be("Running");
            TestConsoleWriterHelper.TestPassed();

            TestConsoleWriterHelper.PrintAssertResult("A Version deve ser '1.0.0'");
            data.Version.Should().Be("1.0.0");
            TestConsoleWriterHelper.TestPassed();
        }
    }
}
