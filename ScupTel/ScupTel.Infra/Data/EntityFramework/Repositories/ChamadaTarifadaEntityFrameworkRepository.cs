using ScupTel.Domain.Chamada;
using System;
using System.Collections.Generic;
using System.Text;
using ScupTel.Infra.Data.EntityFramework.Context;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class ChamadaTarifadaEntityFrameworkRepository : EntityFrameworkRepositoryBase<ChamadaTarifada, Guid>, IChamadaTarifadaRepository
    {
        public ChamadaTarifadaEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }
    }
}
