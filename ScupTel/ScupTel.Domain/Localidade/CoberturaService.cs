using ScupTel.Domain.Core.Models;
using System;

namespace ScupTel.Domain.Localidade
{
    public class CoberturaService : ServiceBase<Cobertura, Guid>, ICoberturaService
    {
        private readonly ICoberturaRepository _coberturaRepository;

        public CoberturaService(ICoberturaRepository coberturaRepository) 
            : base(coberturaRepository)
        {
            _coberturaRepository = coberturaRepository;
        }
    }
}
