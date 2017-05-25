using ScupTel.Domain.Localidade;
using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace ScupTel.Domain.Tarifa
{
    public interface ITarifaChamadaService : IServiceBase<TarifaChamada, Guid>
    {
        ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(int dddOrigem, int dddDestino);
        ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(Cobertura cobertura);
    }
}
