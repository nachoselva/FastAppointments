namespace Common.Infrastructure.Clients
{
    using Common.Models.Appointments;
    using System;
    using System.Net.Http.Json;

    public class AppointmentsClient
    {
        private readonly HttpClient _httpClient;

        public AppointmentsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<GetEventResponse?> GetEvent(Guid sourceId)
        {
            return _httpClient.GetFromJsonAsync<GetEventResponse>($"events/{sourceId}");
        }
    }
}
