using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Produto;
using System;

namespace ScupTel.Domain.Atendimento
{
    public class ClienteProdutoChamadaRelation : Entity<Guid>
    {
        protected ClienteProdutoChamadaRelation() { }

        public ClienteProdutoChamadaRelation(Cliente cliente, ProdutoChamada produtoChamada, bool ativo)
        {
            Cliente = cliente;
            ProdutoChamada = produtoChamada;
            Ativo = ativo;
        }

        public Guid ClienteId { get; protected set; }
        public Guid ProdutoChamadaId { get; protected set; }
        public bool Ativo { get; set; }
        
        public virtual Cliente Cliente { get; set; }
        public virtual ProdutoChamada ProdutoChamada { get; set; }
    }
}
