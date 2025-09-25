namespace Common.Api
{
    using Common.Api.Filters;
    using Microsoft.AspNetCore.Mvc;

    public static class DependencyInjection
    {
        public static MvcOptions AddFilters(this MvcOptions options)
        {
            options.Filters.Add(new ResultActionFilter());

            return options;
        }
    }
}
