using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Chamada;
using ScupTel.Infra.Data.EntityFramework.Extensions;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ChamadaClienteMap : EntityTypeConfiguration<ChamadaCliente>
    {
        public override void Map(EntityTypeBuilder<ChamadaCliente> builder)
        {
            builder.HasOne(o => o.Cliente)
                .WithMany(m => m.Chamadas)
                .HasForeignKey(f => f.ClienteId);
        }
    }
}
