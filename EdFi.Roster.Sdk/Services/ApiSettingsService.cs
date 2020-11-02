using EdFi.Roster.Sdk.Data;
using EdFi.Roster.Sdk.Models;

namespace EdFi.Roster.Sdk.Services
{
    public class ApiSettingsService
    {
        private IDataService _dataService;
        public ApiSettingsService()
        {
            _dataService = new JsonDataFileService();
        }

        public ApiSettings Read()
        {
            return _dataService.Read<ApiSettings>();
        }
        public void Save(ApiSettings apiSettings)
        {
            _dataService.Save(apiSettings);
        }
    }
}
