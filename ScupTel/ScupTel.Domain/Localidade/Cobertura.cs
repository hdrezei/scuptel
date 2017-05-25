using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;

namespace ScupTel.Domain.Localidade
{
    public class Cobertura : Entity<Guid>
    {
        protected Cobertura() { }

        public Cobertura(int dddOrigem, int dddDestino)
        {
            DddOrigem = dddOrigem;
            DddDestino = dddDestino;
            Chamadas = new List<ChamadaBase>();
            TarifasChamada = new List<TarifaChamada>();
        }

        public int DddOrigem { get; protected set; }
        public int DddDestino { get; protected set; }

        public virtual ICollection<ChamadaBase> Chamadas { get; private set; }
        public virtual ICollection<TarifaChamada> TarifasChamada { get; private set; }
    }
}
