using ScupTel.Domain.Localidade;
using System;
using Microsoft.EntityFrameworkCore;
using ScupTel.Infra.Data.EntityFramework.Context;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class CoberturaEntityFrameworkRepository : EntityFrameworkRepositoryBase<Cobertura, Guid>, ICoberturaRepository
    {
        public CoberturaEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }
    }
}
