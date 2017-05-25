using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;

namespace ScupTel.Domain.Tarifa
{
    public class LongaDistanciaNacional : TarifaChamada
    {
        protected LongaDistanciaNacional() { }

        public LongaDistanciaNacional(string nome, Cobertura cobertura, DateTime inicioVigencia, DateTime fimVigencia, decimal valor)
            : base(nome, cobertura, inicioVigencia, fimVigencia, valor)
        {
        }

        internal override bool ValidarTipoDaTarifa(TarifaChamada tarifa)
        {
            return !tarifa.Cobertura.DddOrigem.Equals(tarifa.Cobertura.DddDestino);
        }
    }
}
