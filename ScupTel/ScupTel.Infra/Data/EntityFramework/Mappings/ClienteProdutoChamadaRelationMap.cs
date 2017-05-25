using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScupTel.Domain.Atendimento;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ClienteProdutoChamadaRelationMap : EntityTypeConfiguration<ClienteProdutoChamadaRelation>
    {
        public override void Map(EntityTypeBuilder<ClienteProdutoChamadaRelation> builder)
        {
            builder.HasKey(k => new { k.ClienteId, k.ProdutoChamadaId });

            builder.HasOne(o => o.Cliente)
                .WithMany(m => m.ClienteProdutosChamada)
                .HasForeignKey(f => f.ClienteId);


            builder.HasOne(o => o.ProdutoChamada)
                .WithMany(m => m.ClienteProdutosChamada)
                .HasForeignKey(f => f.ProdutoChamadaId);
        }
    }
}
