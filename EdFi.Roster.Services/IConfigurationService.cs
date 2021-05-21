﻿using System.IO;
using System.Threading.Tasks;
using EdFi.Roster.Sdk.Client;

namespace EdFi.Roster.Services
{
    public interface IConfigurationService
    {
        Task<Configuration> ApiConfiguration();
    }

    public class ConfigurationService : IConfigurationService
    {
        private readonly ApiSettingsService _apiSettingsService;
        private readonly BearerTokenService _bearerTokenService;

        public ConfigurationService(ApiSettingsService apiSettingsService,
            BearerTokenService bearerTokenService)
        {
            _apiSettingsService = apiSettingsService;
            _bearerTokenService = bearerTokenService;
        }

        public async Task<Configuration> ApiConfiguration()
        {
            var apiSettings = await _apiSettingsService.Read();
            var token =  await _bearerTokenService.GetBearerToken(apiSettings);
            return new Configuration
            {
                AccessToken = token,
                BasePath = Path.Combine(apiSettings.RootUrl, "composites/v1")
            };
        }
    }
}