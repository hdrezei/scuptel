using ScupTel.Domain.Core.Interfaces;
using ScupTel.Domain.Produto;
using System;

namespace ScupTel.Domain.Atendimento
{
    public interface IClienteRepository : IRepositoryBase<Cliente, Guid>
    {
        bool AdquirirNumeroTelefone(Cliente cliente, Telefone telefone);
        bool AdquirirProduto(Cliente cliente, ProdutoChamada produto);
    }
}