using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Atendimento
{
    public interface ITelefoneRepository : IRepositoryBase<Telefone, Guid>
    {
    }
}
