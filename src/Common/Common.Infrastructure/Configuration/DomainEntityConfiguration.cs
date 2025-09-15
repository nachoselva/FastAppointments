namespace Common.Infrastructure.Configuration
{
    using Common.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Linq.Expressions;

    public class DomainEntityConfiguration
    {
        public void Configure<T>(EntityTypeBuilder builder)
            where T : DomainEntity
        {
            static string GetColumnName(IMutableEntityType metadata, string propertyName)
            {
                return metadata.FindProperty(propertyName)!.GetColumnName();
            }

            var metadata = builder.Metadata;

            var idColumnName = GetColumnName(metadata, nameof(DomainEntity.Id));
            var createdOnColumnName = GetColumnName(metadata, nameof(DomainEntity.CreatedOn));
            var modifiedOnColumnName = GetColumnName(metadata, nameof(DomainEntity.ModifiedOn));
            var periodStartColumnName = DomainEntityColumnNames.PERIOD_START;
            var periodEndColumnName = DomainEntityColumnNames.PERIOD_END;
            var deletedColumnName = DomainEntityColumnNames.DELETED;

            builder.HasKey(idColumnName);

            builder.Property(idColumnName)
                .ValueGeneratedOnAdd();

            builder.Property<bool>(deletedColumnName)
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired(true);

            builder.Property(createdOnColumnName)
                .HasColumnType("datetime2")
                .IsRequired(true);

            builder.Property(modifiedOnColumnName)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property<DateTime>(periodEndColumnName)
                .HasColumnType("datetime2")
                .IsRequired(true);

            builder.Property<DateTime>(periodStartColumnName)
                .HasColumnType("datetime2")
                .IsRequired(true);

            LambdaExpression test = () => 1;

            builder.HasQueryFilter((T p) => !EF.Property<bool>(p, deletedColumnName));

            builder.ToTable(table => table.IsTemporal(t =>
            {
                t.HasPeriodStart(periodStartColumnName);
                t.HasPeriodEnd(periodEndColumnName);
            }));
        }
    }
}
