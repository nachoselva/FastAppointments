namespace Common.Infrastructure.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class ConfigurationExtensions
    {

        private const string DELETED_FILTER = $"{DomainEntityColumnNames.DELETED} = 0";

        public static string DeletedFilter() => DELETED_FILTER;

        public static IndexBuilder HasDeletedFilter(this IndexBuilder indexBuilder)
        {
            return indexBuilder.HasFilter($"{DomainEntityColumnNames.DELETED} = 0");
        }
    }
}
