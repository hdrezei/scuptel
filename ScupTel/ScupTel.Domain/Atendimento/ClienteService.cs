using System;
using ScupTel.Domain.Produto;
using ScupTel.Domain.Core.Models;

namespace ScupTel.Domain.Atendimento
{
    public class ClienteService : ServiceBase<Cliente, Guid>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
            : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool AdquirirNumeroTelefone(Cliente cliente, Telefone telefone)
        {
            return _clienteRepository.AdquirirNumeroTelefone(cliente, telefone);
        }

        public bool AdquirirProduto(Cliente cliente, ProdutoChamada produto)
        {
            return _clienteRepository.AdquirirProduto(cliente, produto);
        }
    }
}
