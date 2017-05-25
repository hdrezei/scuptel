using ScupTel.Domain.Localidade;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Tarifa;
using System;
using System.Linq;

namespace ScupTel.Domain.Chamada
{
    public class SimulacaoChamadaService : ISimulacaoChamadaService
    {
        private readonly ICoberturaService _coberturaService;
        private readonly ITarifaChamadaService _tarifaChamadaService;
        private readonly IProdutoChamadaService _produtoChamadaService;

        public SimulacaoChamadaService(ICoberturaService coberturaService, ITarifaChamadaService tarifaChamadaService, IProdutoChamadaService produtoChamadaService)
        {
            _coberturaService = coberturaService;
            _tarifaChamadaService = tarifaChamadaService;
            _produtoChamadaService = produtoChamadaService;
        }

        public ChamadaCalculada SimulacaoFaleMais(int dddOrigem, int dddDestino, int tempoChamada, Guid planoId)
        {
            var produtoFaleMais = _produtoChamadaService.Find(o => o.Id.Equals(planoId) && o.GetType() == typeof(FaleMais));

            if (produtoFaleMais == null) { throw new ArgumentException("Não existe um plano do produto FaleMais para o Id informado. ({0})", planoId.ToString()); }

            var simulacaoFaleMais = SimulacaoChamada(dddOrigem, dddDestino, tempoChamada, produtoFaleMais);

            return simulacaoFaleMais;
        }

        public ChamadaCalculada SimulacaoPlanoBasico(int dddOrigem, int dddDestino, int tempoChamada)
        {
            var produtoBasico = _produtoChamadaService.Find(f => f.Nome == "Plano Básico");
            var simulacaoPlanoBasico = SimulacaoChamada(dddOrigem, dddDestino, tempoChamada, produtoBasico);

            return simulacaoPlanoBasico;
        }

        internal ChamadaCalculada SimulacaoChamada(int dddOrigem, int dddDestino, int tempoChamada, ProdutoChamada produtoChamada)
        {
            var cobertura = _coberturaService.Find(o => o.DddOrigem == dddOrigem && o.DddDestino == dddDestino);

            if (cobertura == null)
            {
                return new ChamadaCalculada(null, produtoChamada, null);
            }

            var tarifasChamada = _tarifaChamadaService.BuscarTarifasChamadaPorCobertura(cobertura);
            var chamadaSimulacao = new ChamadaRealizada(cobertura, DateTime.Now, new TimeSpan(0, tempoChamada, 0));
            var chamadaTarifada = new ChamadaTarifada(chamadaSimulacao);

            tarifasChamada.ToList().ForEach(o => chamadaTarifada.Tarifas.Add(o.TarifaCalculada(chamadaTarifada)));

            return produtoChamada.CalculoChamada(chamadaTarifada, 2);
        }
    }
}
