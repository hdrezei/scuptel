using ScupTel.Domain.Chamada;
using System;

namespace ScupTel.Domain.Produto
{
    public class FaleMais : ProdutoChamada
    {
        private readonly decimal PorcentagemAcrescidaNaTarifa = 1.10m;

        protected FaleMais() { }

        public FaleMais(string nome, int minutosFranquia)
            : base(nome)
        {
            MinutosFranquia = minutosFranquia;
        }

        public int MinutosFranquia { get; set; }

        public double TempoDeChamadaTarifado(ChamadaBase chamada)
        {
            return chamada.Duracao.TotalMinutes >= MinutosFranquia ? Math.Round(chamada.Duracao.Subtract(new TimeSpan(0, MinutosFranquia, 0)).TotalMinutes, 2) : 0;
        }

        public decimal ValorTotalDaTarifa(ChamadaTarifada chamadaTarifada)
        {
            return chamadaTarifada.Total() * PorcentagemAcrescidaNaTarifa;
        }

        public override ChamadaCalculada CalculoChamada(ChamadaTarifada chamadaTarifada, int decimais)
        {
            var tarifaTotal = ValorTotalDaTarifa(chamadaTarifada);
            var tempoChamadaTarifado = TempoDeChamadaTarifado(chamadaTarifada.Chamada);
            var valorDaChamada = Convert.ToDecimal(tempoChamadaTarifado) * tarifaTotal;

            return new ChamadaCalculada(chamadaTarifada, this, Math.Round(valorDaChamada, decimais));
        }
    }
}
