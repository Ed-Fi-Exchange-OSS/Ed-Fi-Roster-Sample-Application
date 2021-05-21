using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EdFi.Roster.Sdk.Client;
using EdFi.Roster.Services.ApiSdk;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class SectionService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;
        private readonly IResponseHandleService _responseHandleService;

        public SectionService(IRosterDataService rosterDataService
                            , IConfigurationService configurationService
                            , IResponseHandleService responseHandleService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
            _responseHandleService = responseHandleService;
        }

        public async Task Save(List<Section> sections)
        {
            var sectionsList = sections.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterSection { Content = content }).ToList();
            await _rosterDataService.SaveAsync(sectionsList);
        }

        public async Task<IEnumerable<Section>> ReadAllAsync()
        {
            var sections = await _rosterDataService.ReadAllAsync<RosterSection>();
            return sections.Select(section => JsonConvert.DeserializeObject<Section>(section.Content)).ToList();
        }

        public async Task<ExtendedInfoResponse<List<Section>>> GetAllSectionsWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var sectionsApi = new ApiFacade(apiConfiguration).SectionsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Section>>();
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<Section>> currentApiResponse = null;
                try
                {
                    currentApiResponse = await sectionsApi.GetSectionsAsyncWithHttpInfo(offset, limit);
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                        apiConfiguration = await _configurationService.ApiConfiguration(true);
                        sectionsApi = new ApiFacade(apiConfiguration).SectionsApi;
                        currentApiResponse = await sectionsApi.GetSectionsAsyncWithHttpInfo(offset, limit);
                        errorMessage = string.Empty;
                    }
                }

                if (currentApiResponse == null) continue;
                currResponseRecordCount = currentApiResponse.Data.Count;
                offset += limit;
                response = await _responseHandleService.Handle(currentApiResponse, response, errorMessage);

            } while (currResponseRecordCount >= limit);

            response.GeneralInfo.TotalRecords = response.FullDataSet.Count;
            response.GeneralInfo.ResponseData = JsonConvert.SerializeObject(response.FullDataSet, Formatting.Indented);
            return response;
        }
    }
}
