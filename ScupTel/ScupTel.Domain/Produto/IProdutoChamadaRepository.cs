using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Interfaces;
using System;

namespace ScupTel.Domain.Produto
{
    public interface IProdutoChamadaRepository : IRepositoryBase<ProdutoChamada, Guid>
    {
        ProdutoChamada BuscaProdutoChamadaAtivo(Cliente cliente);
    }
}
