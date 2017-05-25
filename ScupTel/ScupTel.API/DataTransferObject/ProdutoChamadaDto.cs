using ScupTel.API.DataTransferObject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScupTel.API.DataTransferObject
{
    public class ProdutoChamadaDto : IProdutoDto
    {
        public ProdutoChamadaDto(Guid identifier, string nome)
        {
            Identifier = identifier;
            Nome = nome;
        }

        public Guid Identifier { get; set; }
        public string Nome { get; set; }
    }
}
