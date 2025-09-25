namespace Common.Infrastructure.HttpClients
{
    using Common.Models.Setups;
    using System.Net.Http.Json;

    public class SetupsClient(HttpClient httpClient)
    {

        public Task<GetServiceProvisionResponse?> SearchServiceProvision(SearchServiceProvisionRequest request)
        {
            return httpClient.GetFromJsonAsync<GetServiceProvisionResponse>($"serviceProvisions/search" + request.ToQueryString());
        }

        public Task<IEnumerable<GetServiceProvisionResponse?>?> SearchServiceProvisions(SearchServiceProvisionsRequest request)
        {
            return httpClient.GetFromJsonAsync<IEnumerable<GetServiceProvisionResponse?>>($"serviceProvisions/search-many" + request.ToQueryString());
        }
    }
}
