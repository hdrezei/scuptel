using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Localidade;
using ScupTel.Domain.Core.Interfaces;
using System;

namespace ScupTel.Domain.Chamada
{
    public interface IChamadaService : IServiceBase<ChamadaBase, Guid>
    {
        ChamadaCliente RegistrarChamada(Telefone de, int numeroTelefoneDiscado, int dddOrigem, int dddDestino, DateTime inicio, TimeSpan duracao);
    }
}
