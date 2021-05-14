using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class StudentService
    {
        private readonly IRosterDataService _dataService;

        public StudentService(IRosterDataService dataService)
        {
            _dataService = dataService;
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
            var api = new ApiSdk.ApiFacade().StudentsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Student>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await api.GetStudentsAsyncWithHttpInfo(offset, limit);
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
