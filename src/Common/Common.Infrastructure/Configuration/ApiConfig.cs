namespace Common.Infrastructure.Configuration;

public class ApiConfig
{
    public required string Name { get; set; }
    public required string Host { get; set; }
    public required int HttpPort { get; set; }
    public int? HttpsPort { get; set; }
    public required string ApiKey { get; set; }
    public required string Path { get; set; }
}