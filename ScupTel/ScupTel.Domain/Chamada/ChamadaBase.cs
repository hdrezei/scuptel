using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Chamada
{
    public abstract class ChamadaBase : Entity<Guid>
    {
        protected ChamadaBase() { }

        public ChamadaBase(Cobertura cobertura, DateTime inicio, TimeSpan duracao)
        {
            Cobertura = cobertura;
            Inicio = inicio;
            Fim = inicio.AddMinutes(duracao.TotalMinutes);
            ChamadasTarifadas = new List<ChamadaTarifada>();
            ChamadasCliente = new List<ChamadaCliente>();
        }

        public Guid CoberturaId { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public TimeSpan Duracao => Fim.Subtract(Inicio);

        public virtual Cobertura Cobertura { get; private set; }
        public virtual ICollection<ChamadaTarifada> ChamadasTarifadas { get; private set; }
        public virtual ICollection<ChamadaCliente> ChamadasCliente { get; private set; }
    }
}
