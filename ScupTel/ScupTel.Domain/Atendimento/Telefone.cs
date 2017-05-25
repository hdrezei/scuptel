using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using ScupTel.Domain.Chamada;

namespace ScupTel.Domain.Atendimento
{
    public class Telefone : Entity<Guid>
    {
        protected Telefone() { }

        public Telefone(int numero, int dddRegistro, int ddiRegistro, Cliente proprietario)
        {
            Numero = numero;
            DddRegistro = dddRegistro;
            DdiRegistro = ddiRegistro;
            Proprietario = proprietario;
            ChamadasRealizadas = new List<ChamadaCliente>();
            ChamadasRecebidas = new List<ChamadaCliente>();
        }

        public int Numero { get; set; }
        public int DddRegistro { get; set; }
        public int DdiRegistro { get; set; }
        public Guid ClienteId { get; set; }

        public virtual Cliente Proprietario { get; set; }
        public virtual ICollection<ChamadaCliente> ChamadasRealizadas { get; set; }
        public virtual ICollection<ChamadaCliente> ChamadasRecebidas { get; set; }
    }
}