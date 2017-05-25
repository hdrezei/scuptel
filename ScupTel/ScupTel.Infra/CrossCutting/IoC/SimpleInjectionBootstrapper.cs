using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Chamada;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Infra.Data.EntityFramework.Repositories;

namespace ScupTel.Infra.CrossCutting.IoC
{
    public class SimpleInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // App
            services.AddScoped<IMapper, Mapper>();

            // Domain - Service
            services.AddScoped<IChamadaService, ChamadaService>();
            services.AddScoped<ICoberturaService, CoberturaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoChamadaService, ProdutoChamadaService>();
            services.AddScoped<ISimulacaoChamadaService, SimulacaoChamadaService>();
            services.AddScoped<ITarifaChamadaService, TarifaChamadaService>();

            // Infra - Data - Repositories
            services.AddScoped<ScupTelDbContext>();
            services.AddScoped<IChamadaBaseRepository, ChamadaBaseEntityFrameworkRepository>();
            services.AddScoped<IChamadaTarifadaRepository, ChamadaTarifadaEntityFrameworkRepository>();
            services.AddScoped<IClienteRepository, ClienteEntityFrameworkRepository>();
            services.AddScoped<ICoberturaRepository, CoberturaEntityFrameworkRepository>();
            services.AddScoped<IProdutoChamadaRepository, ProdutoChamadaEntityFrameworkRepository>();
            services.AddScoped<IResumoCalculoTarifaChamadaRepository, ResumoCalculoTarifaChamadaEntityFrameworkRepository>();
            services.AddScoped<ITarifaChamadaRepository, TarifaChamadaEntityFrameworkRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneEntityFrameworkRepository>();
        }
    }
}
