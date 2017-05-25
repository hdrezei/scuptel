using ScupTel.Domain.Produto;
using ScupTel.Infra.Data.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScupTel.Infra.Data.EntityFramework.Mappings
{
    public class ProdutoChamadaMap : EntityTypeConfiguration<ProdutoChamada>
    {
        public override void Map(EntityTypeBuilder<ProdutoChamada> builder)
        {
            builder.HasKey(k => k.Id);
        }
    }
}
