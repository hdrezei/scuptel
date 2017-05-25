using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Chamada;
using System;
using ScupTel.Domain.Atendimento;
using System.Collections.Generic;

namespace ScupTel.Domain.Produto
{
    public abstract class ProdutoChamada : Entity<Guid>
    {
        protected ProdutoChamada() { }

        public ProdutoChamada(string nome)
        {
            Nome = nome;
            ClienteProdutosChamada = new List<ClienteProdutoChamadaRelation>();
        }

        public string Nome { get; private set; }
        public ICollection<ClienteProdutoChamadaRelation> ClienteProdutosChamada { get; set; }

        public abstract ChamadaCalculada CalculoChamada(ChamadaTarifada chamadaTarifada, int decimais);
    }
}