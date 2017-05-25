using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScupTel.Domain.Chamada
{
    public class ChamadaTarifada : Entity<Guid>
    {
        protected ChamadaTarifada() { }

        public ChamadaTarifada(ChamadaBase chamada)
        {
            Chamada = chamada;
            Tarifas = new List<ResumoCalculoTarifaChamada>();
        }

        public Guid ChamadaRealizadaId { get; private set; }

        public virtual ChamadaBase Chamada { get; private set; }
        public virtual ICollection<ResumoCalculoTarifaChamada> Tarifas { get; set; }

        public decimal Total()
        {
            return Tarifas.Where(w => w.Aplicado == true).Sum((t) => t.Valor);
        }
    }
}
