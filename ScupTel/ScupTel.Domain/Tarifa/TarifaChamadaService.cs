using ScupTel.Domain.Localidade;
using ScupTel.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Tarifa
{
    public class TarifaChamadaService : ServiceBase<TarifaChamada, Guid>, ITarifaChamadaService
    {
        private readonly ITarifaChamadaRepository _tarifaChamadaRepository;
        private readonly IResumoCalculoTarifaChamadaRepository _resumoCalculoTarifaChamadaRepository;

        public TarifaChamadaService(ITarifaChamadaRepository tarifaChamadaRepository, IResumoCalculoTarifaChamadaRepository resumoCalculoTarifaChamadaRepository)
            : base(tarifaChamadaRepository)
        {
            _tarifaChamadaRepository = tarifaChamadaRepository;
            _resumoCalculoTarifaChamadaRepository = resumoCalculoTarifaChamadaRepository;
        }

        public ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(Cobertura cobertura)
        {
            return BuscarTarifasChamadaPorCobertura(cobertura.DddOrigem, cobertura.DddDestino);
        }

        public ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(int dddOrigem, int dddDestino)
        {
            return _tarifaChamadaRepository.BuscarTarifasChamadaPorCobertura(dddOrigem, dddDestino);
        }
    }
}
