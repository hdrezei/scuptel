using ScupTel.Domain.Localidade;
using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Tarifa
{
    public interface IResumoCalculoTarifaChamadaRepository : IRepositoryBase<ResumoCalculoTarifaChamada, Guid>
    {
    }
}
