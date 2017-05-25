using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Tarifa;
using System;
using System.Linq;
using ScupTel.Domain.Produto;

namespace ScupTel.Domain.Chamada
{
    public class ChamadaService : ServiceBase<ChamadaBase, Guid>, IChamadaService
    {
        private readonly ITarifaChamadaService _tarifaChamadaService;
        private readonly ICoberturaService _coberturaService;
        private readonly IProdutoChamadaService _produtoChamadaService;
        private readonly IChamadaBaseRepository _chamadaBaseRepository;
        private readonly IChamadaTarifadaRepository _chamadaTarifadaRepository;
        private readonly IClienteService _clienteService;

        public ChamadaService(
            ITarifaChamadaService tarifaChamadaService, 
            ICoberturaService coberturaService,
            IProdutoChamadaService produtoChamadaService, 
            IChamadaBaseRepository chamadaBaseRepository,
            IChamadaTarifadaRepository chamadaTarifadaRepository,
            IClienteService clienteService)
            : base(chamadaBaseRepository)
        {
            _tarifaChamadaService = tarifaChamadaService;
            _coberturaService = coberturaService;
            _produtoChamadaService = produtoChamadaService;
            _chamadaBaseRepository = chamadaBaseRepository;
            _chamadaTarifadaRepository = chamadaTarifadaRepository;
            _clienteService = clienteService;
        }

        public ChamadaCliente RegistrarChamada(Telefone de, int numeroTelefoneDiscado, int dddOrigem, int dddDestino, DateTime inicio, TimeSpan duracao)
        {
            var cobertura = _coberturaService.Find(o => o.DddOrigem == dddOrigem && o.DddDestino == dddDestino);
            var chamadaCliente = new ChamadaCliente(de.Proprietario, de.Numero, numeroTelefoneDiscado, cobertura, inicio, duracao);

            chamadaCliente = ((ChamadaCliente)_chamadaBaseRepository.Save(chamadaCliente));

            return chamadaCliente;
        }

        public ChamadaCalculada CalcularChamada(Cliente cliente, int numeroTelefoneCliente, int numeroTelefoneDiscado, int dddOrigem, int dddDestino, DateTime inicio, TimeSpan duracao)
        {
            var cobertura = _coberturaService.Find(o => o.DddOrigem == dddOrigem && o.DddDestino == dddDestino);
            var tarifasChamada = _tarifaChamadaService.BuscarTarifasChamadaPorCobertura(cobertura);
            var chamadaCliente = new ChamadaCliente(cliente, numeroTelefoneDiscado, numeroTelefoneDiscado, cobertura, inicio, duracao);
            var chamadaTarifada = new ChamadaTarifada(chamadaCliente);

            tarifasChamada.ToList().ForEach(o => chamadaTarifada.Tarifas.Add(o.TarifaCalculada(chamadaTarifada)));

            var produtoChamada = _produtoChamadaService.BuscaProdutoChamadaAtivo(cliente);
            var chamadaCalculada = produtoChamada.CalculoChamada(chamadaTarifada, 2);

            chamadaCalculada.ChamadaTarifada = _chamadaTarifadaRepository.Save(chamadaTarifada);

            return chamadaCalculada;
        }
    }
}
