using System;

namespace ScupTel.API.DataTransferObject.Interfaces
{
    public interface IProdutoDto
    {
        Guid Identifier { get; set; }
        string Nome { get; set; }
    }
}
