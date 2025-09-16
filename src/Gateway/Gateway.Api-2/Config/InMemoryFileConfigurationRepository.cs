namespace Gateway.Api.Config
{
    using Ocelot.Configuration.File;
    using Ocelot.Configuration.Repository;
    using Ocelot.Responses;

    public class InMemoryFileConfigurationRepository : IFileConfigurationRepository
    {
        private FileConfiguration _config;

        public InMemoryFileConfigurationRepository(FileConfiguration config)
        {
            _config = config;
        }

        public Task<Response<FileConfiguration>> Get()
        {
            return Task.FromResult<Response<FileConfiguration>>(new OkResponse<FileConfiguration>(_config));
        }

        public Task<Response> Set(FileConfiguration fileConfiguration)
        {
            _config = fileConfiguration;
            return Task.FromResult<Response>(new OkResponse());
        }
    }
}
