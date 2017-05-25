using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Interfaces;
using System;

namespace ScupTel.Domain.Produto
{
    public interface IProdutoChamadaService : IServiceBase<ProdutoChamada, Guid>
    {
        ProdutoChamada BuscaProdutoChamadaAtivo(Cliente cliente);
    }
}
