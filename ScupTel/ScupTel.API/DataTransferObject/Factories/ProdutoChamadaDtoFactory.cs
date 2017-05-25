
using ScupTel.API.DataTransferObject.Interfaces;
using ScupTel.Domain.Produto;
using System;

namespace ScupTel.API.DataTransferObject.Factories
{
    public static class ProdutoChamadaDtoFactory
    {
        public static IProdutoDto ReturnProdutoDto(ProdutoChamada produto)
        {
            if (typeof(FaleMais).Equals(produto.GetType()))
            {
                return new ProdutoChamadaFaleMaisDto((FaleMais)produto);
            }

            return new ProdutoChamadaDto(produto.Id, produto.Nome);
        }
    }
}
