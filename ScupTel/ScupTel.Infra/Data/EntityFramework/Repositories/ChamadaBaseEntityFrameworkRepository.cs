using ScupTel.Domain.Chamada;
using System;
using System.Collections.Generic;
using System.Text;
using ScupTel.Infra.Data.EntityFramework.Context;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class ChamadaBaseEntityFrameworkRepository : EntityFrameworkRepositoryBase<ChamadaBase, Guid>, IChamadaBaseRepository
    {
        public ChamadaBaseEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }
    }
}
