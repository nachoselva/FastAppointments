namespace Common.Infrastructure.HttpClients
{
    using Common.Models.Appointments;
    using System;
    using System.Net.Http.Json;

    public class AppointmentsClient(HttpClient httpClient)
    {
        public Task<GetEventResponse?> GetEvent(Guid sourceId)
        {
            return httpClient.GetFromJsonAsync<GetEventResponse>($"events/{sourceId}");
        }
    }
}
