using ScupTel.API.DataTransferObject.Interfaces;
using ScupTel.Domain.Produto;
using System;

namespace ScupTel.API.DataTransferObject
{
    public class ProdutoChamadaFaleMaisDto : IProdutoDto
    {
        public ProdutoChamadaFaleMaisDto() { }

        public ProdutoChamadaFaleMaisDto(string identifier, string nome, int minutosFranquia)
        {
            Identifier = Guid.Parse(identifier);
            Nome = nome;
            MinutosFranquia = minutosFranquia;
        }

        public ProdutoChamadaFaleMaisDto(Guid identifier, string nome, int minutosFranquia)
        {
            Identifier = identifier;
            Nome = nome;
            MinutosFranquia = minutosFranquia;
        }

        public ProdutoChamadaFaleMaisDto(string nome, int minutosFranquia)
        {
            Nome = nome;
            MinutosFranquia = minutosFranquia;
        }

        public ProdutoChamadaFaleMaisDto(FaleMais produtoFaleMais)
        {
            Identifier = produtoFaleMais.Id;
            Nome = produtoFaleMais.Nome;
            MinutosFranquia = produtoFaleMais.MinutosFranquia;
        }

        public Guid Identifier { get; set; }
        public string Nome { get; set; }
        public int MinutosFranquia { get; set; }
    }
}
