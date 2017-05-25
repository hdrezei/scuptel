using ScupTel.Domain.Produto;
using System;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Domain.Atendimento;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class ProdutoChamadaEntityFrameworkRepository : EntityFrameworkRepositoryBase<ProdutoChamada, Guid>, IProdutoChamadaRepository
    {
        public ProdutoChamadaEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }

        public ProdutoChamada BuscaProdutoChamadaAtivo(Cliente cliente)
        {
            var clienteProdutosChamada = DbSet.Include(i => i.ClienteProdutosChamada)
                .ThenInclude(i => i.Cliente).Where(w => w.ClienteProdutosChamada.Select(o => o.Ativo).Single()).Single();

            return clienteProdutosChamada;                
        }
    }
}
