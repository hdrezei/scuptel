using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Chamada;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ChamadaBaseMap : EntityTypeConfiguration<ChamadaBase>
    {
        public override void Map(EntityTypeBuilder<ChamadaBase> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.Cobertura)
                .WithMany(m => m.Chamadas)
                .HasForeignKey(f => f.CoberturaId);
        }
    }
}
