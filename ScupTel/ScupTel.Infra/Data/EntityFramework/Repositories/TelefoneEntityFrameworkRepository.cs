using ScupTel.Domain.Atendimento;
using System;
using System.Collections.Generic;
using System.Text;
using ScupTel.Infra.Data.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace ScupTel.Infra.Data.EntityFramework.Repositories
{
    public class TelefoneEntityFrameworkRepository : EntityFrameworkRepositoryBase<Telefone, Guid>, ITelefoneRepository
    {
        public TelefoneEntityFrameworkRepository(ScupTelDbContext context) 
            : base(context)
        {
        }
    }
}
