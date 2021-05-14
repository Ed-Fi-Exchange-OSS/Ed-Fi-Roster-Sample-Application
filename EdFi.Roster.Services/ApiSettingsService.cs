using EdFi.Roster.Models;

namespace EdFi.Roster.Services
{
    public class ApiSettingsService
    {
        private IDataService _dataService;

        public ApiSettingsService()
        {
            _dataService = new JsonDataFileService();
        }

        public void Save(ApiSettings apiSettings)
        {
            _dataService.Save(apiSettings);
        }

        public ApiSettings Read()
        {
            var sdkModel = _dataService.Read<ApiSettings>();
            return new ApiSettings { Key = sdkModel.Key, RootUrl = sdkModel.RootUrl, Secret = sdkModel.Secret };
        }
    }
}
