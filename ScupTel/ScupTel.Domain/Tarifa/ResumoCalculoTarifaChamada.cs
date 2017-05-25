using ScupTel.Domain.Chamada;
using ScupTel.Domain.Core.Models;
using System;

namespace ScupTel.Domain.Tarifa
{
    public class ResumoCalculoTarifaChamada : Entity<Guid>
    {
        protected ResumoCalculoTarifaChamada() { }

        public ResumoCalculoTarifaChamada(ChamadaTarifada chamadaTarifada, TarifaChamada tarifaChamada, decimal valor, bool aplicado)
        {
            ChamadaTarifada = chamadaTarifada;
            TarifaChamada = tarifaChamada;
            Valor = valor;
            Aplicado = aplicado;
        }

        public Guid ChamadaTarifadaId { get; private set; }
        public Guid TarifaChamadaId { get; private set; }
        public decimal Valor { get; private set; }
        public bool Aplicado { get; private set; }

        public virtual ChamadaTarifada ChamadaTarifada { get; private set; }
        public virtual TarifaChamada TarifaChamada { get; private set; }
    }
}
