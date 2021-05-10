using EdFi.Roster.Data;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class SchoolService
    {
        private readonly IDataService _dataService;

        public SchoolService(IDataService dataService)
        {
            _dataService = dataService;
        }
        public void Save(List<School> schools)
        {
            _dataService.SaveAsync(schools);
        }

        public async Task<IEnumerable<School>> ReadAllAsync()
        {
            return await _dataService.ReadAsync<List<School>>();
        }
        public async Task<ExtendedInfoResponse<List<School>>> GetAllSchoolsWithExtendedInfoAsync()
        {
            var api = new ApiSdk.ApiFacade().SchoolsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<School>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await api.GetSchoolsAsyncWithHttpInfo(offset, limit);
                currResponseRecordCount = currResponse.Data.Count;
                offset += limit;
                var responsePage = new ExtendedInfoResponsePage<List<School>>
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
