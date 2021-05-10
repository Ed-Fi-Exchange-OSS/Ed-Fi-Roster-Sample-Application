using EdFi.Roster.Data;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class LocalEducationAgencyService
    {
        private readonly IDataService _dataService;

        public LocalEducationAgencyService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Save(List<LocalEducationAgency> localEducationAgencies)
        {
            _dataService.SaveAsync(localEducationAgencies);
        }

        public async Task<IEnumerable<LocalEducationAgency>> ReadAllAsync()
        {
            return await _dataService.ReadAsync<List<LocalEducationAgency>>();
        }

        public List<LocalEducationAgency> GetAllFromApi()
        {
            var api = new ApiSdk.ApiFacade();
            return api.LocalEducationAgenciesApi.GetLocalEducationAgencies();
        }

        public async Task<ExtendedInfoResponse<List<LocalEducationAgency>>> GetAllLocalEducationAgenciesWithExtendedInfoAsync()
        {
            var leaApi = new ApiSdk.ApiFacade().LocalEducationAgenciesApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<LocalEducationAgency>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await leaApi.GetLocalEducationAgenciesAsyncWithHttpInfo(offset, limit);
                currResponseRecordCount = currResponse.Data.Count;
                offset += limit;
                var responsePage = new ExtendedInfoResponsePage<List<LocalEducationAgency>>
                {
                    Data = currResponse.Data,
                    ResponseUri = currResponse.ResponseUri
                };
                response.Pages.Add(responsePage);
                response.FullDataSet.AddRange(currResponse.Data);
            } while (currResponseRecordCount >= limit);

            var distinctSectionIds = response.FullDataSet.Select(x => x.Id).Distinct();
            return response;
        }
    }
}

