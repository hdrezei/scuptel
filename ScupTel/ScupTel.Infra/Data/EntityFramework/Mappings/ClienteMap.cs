using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Atendimento;
using ScupTel.Infra.Data.EntityFramework.Extensions;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(k => k.Id);
            
            builder.Property(c => c.Nome)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .HasColumnType("bit")
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}
