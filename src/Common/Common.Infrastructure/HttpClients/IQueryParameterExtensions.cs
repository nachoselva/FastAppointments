namespace Common.Infrastructure.HttpClients
{
    using Common.Models.Extensions;
    using System.Reflection;
    using System.Web;

    public static class QueryParameterExtensions
    {
        public static string ToQueryString<T>(this T queryParameter)
            where T : IQueryParameter
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var keyValuePairs = properties.SelectMany(p =>
            {
                var value = p.GetValue(queryParameter);
                if (value == null)
                    return [];

                if (value is System.Collections.IEnumerable enumerable && value is not string)
                {
                    var items = new List<string>();
                    int index = 0;
                    foreach (var item in enumerable)
                    {
                        if (item != null)
                            items.Add($"{HttpUtility.UrlEncode(p.Name)}[{index}]={HttpUtility.UrlEncode(item.ToString())}");
                        index++;
                    }
                    return items;
                }

                return [$"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(value.ToString())}"];
            });

            return "?" + string.Join("&", keyValuePairs);
        }
    }
}
