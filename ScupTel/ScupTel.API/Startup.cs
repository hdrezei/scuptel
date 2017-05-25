using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.CrossCutting.IoC;
using ScupTel.Infra.Data.EntityFramework.Initializers;

namespace ScupTel.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ScupTelDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));            

            // Add framework services.
            services.AddMvc();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ScupTelDbContext>();
                EntityFrameworkDbInitializer.Initialize(context);
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=get}/{id?}");

                routes.MapRoute(
                    name: "chamadas",
                    template: "chamadas/{action}",
                    defaults: new { controller = "Chamadas" });
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            SimpleInjectorBootstrapper.RegisterServices(services);
        }
    }
}
