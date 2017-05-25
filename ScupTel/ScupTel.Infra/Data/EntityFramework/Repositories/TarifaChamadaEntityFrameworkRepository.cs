using ScupTel.Domain.Tarifa;
using System;
using System.Collections.Generic;
using System.Text;
using ScupTel.Infra.Data.EntityFramework.Context;
using ScupTel.Domain.Localidade;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class TarifaChamadaEntityFrameworkRepository : EntityFrameworkRepositoryBase<TarifaChamada, Guid>, ITarifaChamadaRepository
    {
        public TarifaChamadaEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }

        public ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(Cobertura cobertura)
        {
            return BuscarTarifasChamadaPorCobertura(cobertura.DddOrigem, cobertura.DddDestino);
        }

        public ICollection<TarifaChamada> BuscarTarifasChamadaPorCobertura(int dddOrigem, int dddDestino)
        {
            return DbSet.Include(i => i.Cobertura).Where(w => w.Cobertura.DddOrigem.Equals(dddOrigem) && w.Cobertura.DddDestino.Equals(dddDestino)).ToList();
        }
    }
}
