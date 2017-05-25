using ScupTel.Domain.Core.Models;
using ScupTel.Domain.Localidade;
using System;
using System.Collections.Generic;

namespace ScupTel.Domain.Chamada
{
    public class ChamadaRealizada : ChamadaBase
    {
        protected ChamadaRealizada() { }

        public ChamadaRealizada(Cobertura cobertura, DateTime inicio, TimeSpan duracao)
            : base(cobertura, inicio, duracao)
        {
        }
    }
}
