using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ResumoCalculoTarifaChamadaMap : EntityTypeConfiguration<ResumoCalculoTarifaChamada>
    {
        public override void Map(EntityTypeBuilder<ResumoCalculoTarifaChamada> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.ChamadaTarifada)
                .WithMany(m => m.Tarifas)
                .HasForeignKey(f => f.ChamadaTarifadaId);


            builder.HasOne(o => o.TarifaChamada)
                .WithMany(m => m.ResumoTarifasAplicadas)
                .HasForeignKey(f => f.TarifaChamadaId);
        }
    }
}
