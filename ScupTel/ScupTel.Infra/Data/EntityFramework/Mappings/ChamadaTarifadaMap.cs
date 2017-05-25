using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Chamada;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ChamadaTarifadaMap : EntityTypeConfiguration<ChamadaTarifada>
    {
        public override void Map(EntityTypeBuilder<ChamadaTarifada> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.Chamada)
                .WithMany(m => m.ChamadasTarifadas)
                .HasForeignKey(f => f.ChamadaRealizadaId);
        }
    }
}
