namespace Common.Infrastructure.EFConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class EFConfigurationExtensions
    {

        private const string DELETED_FILTER = $"{DomainEntityColumnNames.DELETED} = 0";

        public static string DeletedFilter() => DELETED_FILTER;

        public static IndexBuilder HasDeletedFilter(this IndexBuilder indexBuilder)
        {
            return indexBuilder.HasFilter(DELETED_FILTER);
        }
    }
}
