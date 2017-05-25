using ScupTel.Domain.Tarifa;
using System;
using ScupTel.Infra.Data.EntityFramework.Context;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class ResumoCalculoTarifaChamadaEntityFrameworkRepository : EntityFrameworkRepositoryBase<ResumoCalculoTarifaChamada, Guid>, IResumoCalculoTarifaChamadaRepository
    {
        public ResumoCalculoTarifaChamadaEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }
    }
}
