using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.Data.EntityFramework.Initializers;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ScupTel.Tests.IntegrationsTests.Configuration
{
    [Collection("BaseCollection")]
    public abstract class BaseIntegrationTest
    {
        protected readonly TestServer Server;
        protected readonly HttpClient HttpClient;
        protected readonly ScupTelDbContext TestDbContext;
        protected BaseTestFixture Fixture { get; }

        protected BaseIntegrationTest(BaseTestFixture fixture)
        {
            Fixture = fixture;

            TestDbContext = fixture.TestDbContext;
            Server = fixture.Server;
            HttpClient = fixture.HttpClient;

            ClearDb().Wait();
        }

        private async Task ClearDb()
        {
            var commands = new[]
            {
                "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                "EXEC sp_MSForEachTable 'DELETE FROM ? '",
                "EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'"
            };

            await TestDbContext.Database.OpenConnectionAsync();

            foreach(var command in commands)
            {
                await TestDbContext.Database.ExecuteSqlCommandAsync(command);
            }

            EntityFrameworkTestDbInitializer.Initialize(TestDbContext);
            TestDbContext.Database.CloseConnection();
        }
    }
}
