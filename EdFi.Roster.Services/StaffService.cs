using EdFi.Roster.Data;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class StaffService
    {
        private readonly IDataService _dataService;
        public StaffService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<Staff> ReadAll()
        {
            return _dataService.ReadAsync<List<Staff>>().Result;
        }

        public void Save(List<Staff> staff)
        {
            _dataService.SaveAsync(staff);
        }

        public async Task<ExtendedInfoResponse<List<Staff>>> GetAllStaffWithExtendedInfoAsync()
        {
            var api = new ApiSdk.ApiFacade().StaffsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Staff>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await api.GetStaffsAsyncWithHttpInfo(offset, limit);
                currResponseRecordCount = currResponse.Data.Count;
                offset += limit;
                var responsePage = new ExtendedInfoResponsePage<List<Staff>>
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
