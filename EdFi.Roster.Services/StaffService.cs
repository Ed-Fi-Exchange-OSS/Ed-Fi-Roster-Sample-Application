using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class StaffService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;

        public StaffService(IRosterDataService rosterDataService
            , IConfigurationService configurationService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
        }

        public async Task<IEnumerable<Staff>> ReadAllAsync()
        {
            var staff = await _rosterDataService.ReadAllAsync<RosterStaff>();
            return staff.Select(st => JsonConvert.DeserializeObject<Staff>(st.Content)).ToList();
        }

        public async Task Save(List<Staff> staff)
        {
            var staffList = staff.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterStaff { Content = content }).ToList();
            await _rosterDataService.SaveAsync(staffList);
        }

        public async Task<ExtendedInfoResponse<List<Staff>>> GetAllStaffWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var api = new ApiSdk.ApiFacade(apiConfiguration).StaffsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Staff>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await api.GetStaffsWithHttpInfoAsync(offset, limit);
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
