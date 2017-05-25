using ScupTel.Domain.Atendimento;
using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;

namespace ScupTel.Domain.Chamada
{
    public class ChamadaCliente : ChamadaBase
    {
        protected ChamadaCliente() { } 

        public ChamadaCliente(Cliente cliente, int numeroTelefoneCliente, int numeroTelefoneDiscado, Cobertura cobertura, DateTime inicio, TimeSpan duracao)
            : base(cobertura, inicio, duracao)
        {
            Cliente = cliente;
            NumeroTelefoneCliente = numeroTelefoneCliente;
            NumeroTelefoneDiscado = numeroTelefoneDiscado;
        }

        public Guid ClienteId { get; private set; }
        public long NumeroTelefoneCliente { get; private set; }
        public long NumeroTelefoneDiscado { get; private set; }

        public virtual Cliente Cliente { get; private set; }
    }
}
