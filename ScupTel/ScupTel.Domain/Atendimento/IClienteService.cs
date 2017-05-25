using ScupTel.Domain.Core.Interfaces;
using ScupTel.Domain.Produto;
using System;

namespace ScupTel.Domain.Atendimento
{
    public interface IClienteService : IServiceBase<Cliente, Guid>
    {
        bool AdquirirProduto(Cliente cliente, ProdutoChamada produto);
        bool AdquirirNumeroTelefone(Cliente cliente, Telefone telefone);
    }
}
