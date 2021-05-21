using System;
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
    public class StudentService
    {
        private readonly IRosterDataService _dataService;
        private readonly IConfigurationService _configurationService;
        private readonly IResponseHandleService _responseHandleService;

        public StudentService(IRosterDataService dataService
            , IConfigurationService configurationService
            , IResponseHandleService responseHandleService)
        {
            _dataService = dataService;
            _configurationService = configurationService;
            _responseHandleService = responseHandleService;
        }

        public async Task<IEnumerable<Student>> ReadAllAsync()
        {
            var students = await _dataService.ReadAllAsync<RosterStudent>();
            return students.Select(st => JsonConvert.DeserializeObject<Student>(st.Content)).ToList();
        }

        public async Task Save(List<Student> students)
        {
            var studentList = students.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterStudent { Content = content }).ToList();
            await _dataService.SaveAsync(studentList);
        }

        public async Task<ExtendedInfoResponse<List<Student>>> GetAllStudentsWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var api = new ApiFacade(apiConfiguration).StudentsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Student>>();
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<Student>> currentApiResponse = null;
                try
                {
                    currentApiResponse = await api.GetStudentsAsyncWithHttpInfo(offset, limit);
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                        apiConfiguration = await _configurationService.ApiConfiguration(true);
                        api = new ApiFacade(apiConfiguration).StudentsApi;
                        currentApiResponse = await api.GetStudentsAsyncWithHttpInfo(offset, limit);
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
