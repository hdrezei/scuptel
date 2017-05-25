using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Produto
{
    public class ProdutoChamadaService : ServiceBase<ProdutoChamada, Guid>, IProdutoChamadaService
    {
        private readonly IProdutoChamadaRepository _produtoChamadaRepository;

        public ProdutoChamadaService(IProdutoChamadaRepository produtoChamadaRepository)
            : base(produtoChamadaRepository)
        {
            _produtoChamadaRepository = produtoChamadaRepository;
        }

        public ProdutoChamada BuscaProdutoChamadaAtivo(Cliente cliente)
        {
            return _produtoChamadaRepository.BuscaProdutoChamadaAtivo(cliente);
        }
    }
}
