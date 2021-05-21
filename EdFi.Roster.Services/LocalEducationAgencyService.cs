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
    public class LocalEducationAgencyService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;
        private readonly IResponseHandleService _responseHandleService;

        public LocalEducationAgencyService(IRosterDataService rosterDataService
                , IConfigurationService configurationService
                , IResponseHandleService responseHandleService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
            _responseHandleService = responseHandleService;
        }

        public async Task Save(List<LocalEducationAgency> localEducationAgencies)
        {
            var leas = localEducationAgencies.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterLocalEducationAgency {Content = content}).ToList();

             await _rosterDataService.SaveAsync(leas);
        }

        public async Task<IEnumerable<LocalEducationAgency>> ReadAllAsync()
        {
            var leas = await _rosterDataService.ReadAllAsync<RosterLocalEducationAgency>();
            return leas.Select(lea => JsonConvert.DeserializeObject<LocalEducationAgency>(lea.Content)).ToList();
        }

        public async Task<ExtendedInfoResponse<List<LocalEducationAgency>>> GetAllLocalEducationAgenciesWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var leaApi = new ApiFacade(apiConfiguration).LocalEducationAgenciesApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<LocalEducationAgency>>();
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<LocalEducationAgency>> currentApiResponse = null;
                try
                {
                    currentApiResponse = await leaApi.GetLocalEducationAgenciesWithHttpInfoAsync(offset, limit);
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                         apiConfiguration = await _configurationService.ApiConfiguration(true);
                         leaApi = new ApiFacade(apiConfiguration).LocalEducationAgenciesApi;
                         currentApiResponse = await leaApi.GetLocalEducationAgenciesWithHttpInfoAsync(offset, limit);
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

