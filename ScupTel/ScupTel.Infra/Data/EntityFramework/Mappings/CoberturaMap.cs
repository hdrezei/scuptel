using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Localidade;
using ScupTel.Infra.Data.EntityFramework.Extensions;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class CoberturaMap : EntityTypeConfiguration<Cobertura>
    {
        public override void Map(EntityTypeBuilder<Cobertura> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasIndex(k => new { k.DddOrigem, k.DddDestino })
                .IsUnique();
        }
    }
}
