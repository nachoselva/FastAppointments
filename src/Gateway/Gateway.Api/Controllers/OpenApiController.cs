using Common.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace Gateway.Api.Controllers;

[ApiController]
[Route("[controller]")]
public partial class OpenApiController : ControllerBase
{
    private const string OPEN_API_CACHE_KEY = "OPEN_API_GATEWAY_CACHE_KEY";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;

    public OpenApiController(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _cache = cache;
    }

    [HttpGet]
    [Route("v1.json")]
    public async Task<IActionResult> Get()
    {
        if (!_cache.TryGetValue(OPEN_API_CACHE_KEY, out OpenApiDocument? mergedDocument) || mergedDocument == null)
        {
            mergedDocument = await BuildOpenApiDocument();

            _cache.Set(OPEN_API_CACHE_KEY, mergedDocument, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
        }

        var result = mergedDocument!.SerializeAsJson(Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0);
        return Content(result, "application/json");
    }

    private async Task<OpenApiDocument> BuildOpenApiDocument()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var mergedDocument = new OpenApiDocument
        {
            Info = new OpenApiInfo { Title = "FastAppointments_Gateway.Api | v1", Version = "1.0.0" },
            Paths = [],
            Components = new OpenApiComponents()
        };

        var apiConfigs = _configuration.GetApisConfig();

        foreach (var item in apiConfigs)
        {
            string paymentsJson = await httpClient.GetStringAsync($"http://{item.Host}:{item.HttpPort}/openapi/v1.json");
            var reader = new OpenApiStringReader();
            var doc = reader.Read(paymentsJson, out _);

            foreach (var path in doc.Paths)
            {
                string newPath = $"/{item.Path}{path.Key}";
                mergedDocument.Paths.Add(newPath, path.Value);
            }

            if (doc.Components != null)
            {
                foreach (var schema in doc.Components.Schemas)
                    mergedDocument.Components.Schemas[schema.Key] = schema.Value;

                foreach (var response in doc.Components.Responses)
                    mergedDocument.Components.Responses[response.Key] = response.Value;

                foreach (var param in doc.Components.Parameters)
                    mergedDocument.Components.Parameters[param.Key] = param.Value;

                foreach (var rb in doc.Components.RequestBodies)
                    mergedDocument.Components.RequestBodies[rb.Key] = rb.Value;

                foreach (var sec in doc.Components.SecuritySchemes)
                    mergedDocument.Components.SecuritySchemes[sec.Key] = sec.Value;

                foreach (var header in doc.Components.Headers)
                    mergedDocument.Components.Headers[header.Key] = header.Value;

                foreach (var example in doc.Components.Examples)
                    mergedDocument.Components.Examples[example.Key] = example.Value;

                foreach (var link in doc.Components.Links)
                    mergedDocument.Components.Links[link.Key] = link.Value;

                foreach (var cb in doc.Components.Callbacks)
                    mergedDocument.Components.Callbacks[cb.Key] = cb.Value;
            }
        }

        return mergedDocument;
    }
}