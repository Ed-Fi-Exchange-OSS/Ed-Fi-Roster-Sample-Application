using EdFi.Roster.Sdk.Models;

namespace EdFi.Roster.Services
{
    public class ApiSettingsService
    {
        //this is pretty much a pass through so that the web/Explorer project does not have to reference the sdk directly
        public void Save(ApiSettings apiSettings)
        {
            new EdFi.Roster.Sdk.Services.ApiSettingsService().Save(apiSettings);
        }

        public ApiSettings Read()
        {
            var sdkModel = new EdFi.Roster.Sdk.Services.ApiSettingsService().Read();
            return new ApiSettings { Key = sdkModel.Key, RootUrl = sdkModel.RootUrl, Secret = sdkModel.Secret };
        }
    }
}
