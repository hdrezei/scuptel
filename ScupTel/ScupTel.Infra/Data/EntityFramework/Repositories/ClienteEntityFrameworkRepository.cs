using ScupTel.Domain.Atendimento;
using System;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Domain.Produto;
using Microsoft.EntityFrameworkCore;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class ClienteEntityFrameworkRepository : EntityFrameworkRepositoryBase<Cliente, Guid>, IClienteRepository
    {
        private readonly ITelefoneRepository _telefoneRepository;

        public ClienteEntityFrameworkRepository(ScupTelDbContext context, ITelefoneRepository telefoneRepository) 
            : base(context)
        {
            _telefoneRepository = telefoneRepository;
        }

        public bool AdquirirNumeroTelefone(Cliente cliente, Telefone telefone)
        {
            cliente.NumerosTelefone.Add(telefone);
            Save(cliente);

            return true;
        }

        public bool AdquirirProduto(Cliente cliente, ProdutoChamada produto)
        {
            var clienteProdutoChamadaRelation = new ClienteProdutoChamadaRelation(cliente, produto, true);
            cliente.ClienteProdutosChamada.Add(clienteProdutoChamadaRelation);
            Save(cliente);

            return true;
        }
    }
}
