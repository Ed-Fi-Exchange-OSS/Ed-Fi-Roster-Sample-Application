using System;
using System.Threading.Tasks;

namespace EdFi.Roster.Services.ApiSdk
{
    public interface IApiFacade
    {
        Task<T> GetApiClassInstance<T>(bool refreshToken = false);
    }

    public class ApiFacade : IApiFacade
    {
        private readonly IConfigurationService _configurationService;

        public ApiFacade(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<T> GetApiClassInstance<T>(bool refreshToken = false)
        {
            var apiConfiguration = await _configurationService.ApiConfiguration(refreshToken);
            return (T)Activator.CreateInstance(typeof(T), apiConfiguration);
        }
    }
}
