using System;
using ScupTel.Domain.Chamada;

namespace ScupTel.Domain.Produto
{
    public class PlanoBasico : ProdutoChamada
    {
        protected PlanoBasico() { }
        public PlanoBasico(string nome) : base(nome) { }

        public override ChamadaCalculada CalculoChamada(ChamadaTarifada chamadaTarifada, int decimais)
        {
            var tempoLigacao = Math.Round(chamadaTarifada.Chamada.Duracao.TotalMinutes);
            var tarifa = chamadaTarifada.Total();
            var valorDaChamada = Convert.ToDecimal(tempoLigacao) * tarifa;

            return new ChamadaCalculada(chamadaTarifada, this, Math.Round(valorDaChamada, decimais));
        }
    }
}
