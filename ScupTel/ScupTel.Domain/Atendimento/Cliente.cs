using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Produto;
using System;
using System.Collections.Generic;

namespace ScupTel.Domain.Atendimento
{
    public class Cliente : Entity<Guid>
    {
        protected Cliente() { }

        public Cliente(string nome)
        {
            Nome = nome;
            Ativo = true;
            NumerosTelefone = new List<Telefone>();
            ClienteProdutosChamada = new List<ClienteProdutoChamadaRelation>();
            Chamadas = new List<ChamadaCliente>();
        }

        public string Nome { get; internal set; }
        public bool Ativo { get; internal set; }

        public virtual ICollection<Telefone> NumerosTelefone { get; set; }
        public virtual ICollection<ClienteProdutoChamadaRelation> ClienteProdutosChamada { get; set; }
        public virtual ICollection<ChamadaCliente> Chamadas { get; set; }

        public string NumeroTelefoneCompleto(Telefone telefone)
        {
            return string.Concat("+", telefone.DdiRegistro, " (", telefone.DddRegistro, ") ", telefone.Numero);
        }
    }
}
