using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Core.Interfaces
{
    public interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; set; }
        DateTime DataDeRegistro { get; set; }
    }
}
