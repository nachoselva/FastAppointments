namespace Common.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Payments.Api.Filters;

    public static class DependencyInjection
    {
        public static MvcOptions AddFilters(this MvcOptions options)
        {
            options.Filters.Add(new ResultActionFilter());

            return options;
        }
    }
}
