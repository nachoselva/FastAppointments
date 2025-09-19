namespace Common.Infrastructure.Clients
{
    public class PaymentsClient
    {
        private readonly HttpClient _httpClient;

        public PaymentsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
