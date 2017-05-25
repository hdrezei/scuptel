using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using ScupTel.API;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.Data.EntityFramework.Initializers;
using System;
using System.IO;
using System.Net.Http;

namespace ScupTel.Tests.IntegrationsTests.Configuration 
{
    public class BaseTestFixture : IDisposable
    {
        public readonly TestServer Server;
        public readonly HttpClient HttpClient;
        public readonly ScupTelDbContext TestDbContext;
        public readonly IConfigurationRoot Configuration;

        public BaseTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            var options = new DbContextOptionsBuilder<ScupTelDbContext>();
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            TestDbContext = new ScupTelDbContext(options.Options);
            
            if (TestDbContext.GetService<IRelationalDatabaseCreator>().Exists())
            {
                // Drop if changed
                TestDbContext.Database.EnsureDeleted();
            }

            SetupDatabase();

            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient = Server.CreateClient();
        }

        private void SetupDatabase()
        {
            try
            {
                TestDbContext.Database.Migrate();
                EntityFrameworkTestDbInitializer.Initialize(TestDbContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            TestDbContext.Dispose();
            HttpClient.Dispose();
            Server.Dispose();
        }
    }
}
