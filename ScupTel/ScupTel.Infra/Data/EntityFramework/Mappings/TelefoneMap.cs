using ScupTel.Domain.Atendimento;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class TelefoneMap : EntityTypeConfiguration<Telefone>
    {
        public override void Map(EntityTypeBuilder<Telefone> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(c => c.Proprietario)
                .WithMany(m => m.NumerosTelefone)
                .HasForeignKey(f => f.ClienteId);
        }
    }
}
