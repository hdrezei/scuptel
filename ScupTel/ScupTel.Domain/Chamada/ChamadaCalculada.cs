using ScupTel.Domain.Produto;
using ScupTel.Domain.Core.Models;

namespace ScupTel.Domain.Chamada
{
    public class ChamadaCalculada : ValueObject
    {
        public ChamadaCalculada(ChamadaTarifada chamadaTarifada, ProdutoChamada produto, decimal? valor)
        {
            ChamadaTarifada = chamadaTarifada;
            Produto = produto;
            Valor = valor;
        }

        public ChamadaTarifada ChamadaTarifada { get; set; }
        public ProdutoChamada Produto { get; set; }
        public decimal? Valor { get; set; }
    }
}
