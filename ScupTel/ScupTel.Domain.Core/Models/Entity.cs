using ScupTel.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Domain.Core.Models
{
    public abstract class Entity<TIdentifier> : IEntity<TIdentifier>
    {
        public TIdentifier Id { get; set; }
        public DateTime DataDeRegistro { get; set; }
    }
}
