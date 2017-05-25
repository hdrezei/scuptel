using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScupTel.Infra.Data.EntityFramework.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
