namespace Common.Infrastructure.Configuration
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static ApiConfig[] GetApisConfig(this IConfiguration configuration)
        {
            return configuration.GetRequiredSection("Apis").Get<ApiConfig[]>() ?? [];
        }

        public static IDictionary<string, ApiConfig> GetApisConfigByName(this IConfiguration configuration)
        {
            return configuration.GetApisConfig().ToDictionary(ap => ap.Name, ap => ap);
        }
    }
}
