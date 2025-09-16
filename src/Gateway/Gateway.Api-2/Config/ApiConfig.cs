namespace Gateway.Api.Controllers;

public partial class OpenApiController
{
    public class ApiConfig
    {
        public required string Url { get; set; }
        public required int Port { get; set; }
        public required string ApiKey { get; set; }
        public required string Path { get; set; }
    }
}