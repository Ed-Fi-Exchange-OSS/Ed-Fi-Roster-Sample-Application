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
    public class StaffService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;
        private readonly IResponseHandleService _responseHandleService;

        public StaffService(IRosterDataService rosterDataService
            , IConfigurationService configurationService
            , IResponseHandleService responseHandleService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
            _responseHandleService = responseHandleService;
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
            var api = new ApiFacade(apiConfiguration).StaffsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Staff>>();
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<Staff>> currentApiResponse = null;
                try
                {
                    currentApiResponse = await api.GetStaffsAsyncWithHttpInfo(offset, limit);
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                        apiConfiguration = await _configurationService.ApiConfiguration(true);
                        api = new ApiFacade(apiConfiguration).StaffsApi;
                        currentApiResponse = await api.GetStaffsAsyncWithHttpInfo(offset, limit);
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
