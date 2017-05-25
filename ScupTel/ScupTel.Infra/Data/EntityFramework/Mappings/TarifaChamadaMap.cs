using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Tarifa;
using ScupTel.Infra.Data.EntityFramework.Extensions;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class TarifaChamadaMap : EntityTypeConfiguration<TarifaChamada>
    {
        public override void Map(EntityTypeBuilder<TarifaChamada> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.Cobertura)
                .WithMany(m => m.TarifasChamada)
                .HasForeignKey(f => f.CoberturaId);
        }
    }
}
