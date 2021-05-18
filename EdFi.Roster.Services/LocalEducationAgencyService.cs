using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdFi.Roster.Services.ApiSdk;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class LocalEducationAgencyService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;

        public LocalEducationAgencyService(IRosterDataService rosterDataService
                , IConfigurationService configurationService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
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
            int currResponseRecordCount;

            do
            {
                var currResponse = await leaApi.GetLocalEducationAgenciesWithHttpInfoAsync(offset, limit);
                currResponseRecordCount = currResponse.Data.Count;
                offset += limit;
                var responsePage = new ExtendedInfoResponsePage
                {
                    RecordsCount = currResponse.Data.Count,
                    ResponseUri = currResponse.ResponseUri
                };
                response.GeneralInfo.Pages.Add(responsePage);
                response.FullDataSet.AddRange(currResponse.Data);
            } while (currResponseRecordCount >= limit);

            response.GeneralInfo.TotalRecords = response.FullDataSet.Count;
            response.GeneralInfo.ResponseData = JsonConvert.SerializeObject(response.FullDataSet, Formatting.Indented);

            return response;
        }
    }
}

