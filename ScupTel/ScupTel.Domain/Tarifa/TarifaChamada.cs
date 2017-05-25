using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;
using ScupTel.Domain.Chamada;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ScupTel.Tests.UnitTests")]
[assembly: InternalsVisibleTo("ScupTel.Tests.IntegrationTests")]

namespace ScupTel.Domain.Tarifa
{
    public abstract class TarifaChamada : Entity<Guid>
    {
        protected TarifaChamada() { }

        protected TarifaChamada(string nome, Cobertura cobertura, DateTime inicioVigencia, DateTime fimVigencia, decimal valorBaseCalculo)
        {
            Nome = nome;
            Cobertura = cobertura;
            InicioVigencia = inicioVigencia;
            FimVigencia = fimVigencia;
            ValorBaseCalculo = valorBaseCalculo;
            ResumoTarifasAplicadas = new List<ResumoCalculoTarifaChamada>();
        }

        public string Nome { get; protected set; }
        public Guid CoberturaId { get; protected set; }
        public DateTime InicioVigencia { get; protected set; }
        public DateTime FimVigencia { get; protected set; }
        public decimal ValorBaseCalculo { get; protected set; }

        public virtual Cobertura Cobertura { get; protected set; }
        public virtual ICollection<ResumoCalculoTarifaChamada> ResumoTarifasAplicadas { get; protected set; }

        internal abstract bool ValidarTipoDaTarifa(TarifaChamada tarifaChamada);

        internal virtual decimal CalcularTarifaChamada(TarifaChamada tarifaChamada)
        {
            return ValorBaseCalculo;
        }

        internal bool TarifaVigente(ChamadaTarifada chamadaTarifada)
        {
            var agora = DateTime.Now;
            return InicioVigencia < chamadaTarifada.Chamada.Fim && FimVigencia > chamadaTarifada.Chamada.Inicio;
        }

        public ResumoCalculoTarifaChamada TarifaCalculada(ChamadaTarifada chamadaTarifada)
        {
            return new ResumoCalculoTarifaChamada(chamadaTarifada, this, CalcularTarifaChamada(this), ValidarTipoDaTarifa(this) && TarifaVigente(chamadaTarifada));
        }
    }
}
