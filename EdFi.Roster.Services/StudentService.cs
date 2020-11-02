using EdFi.Roster.Data;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class StudentService
    {
        private readonly IDataService _dataService;

        public StudentService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IEnumerable<Student> ReadAll()
        {
            return _dataService.ReadAsync<List<Student>>().Result;
        }

        public void Save(List<Student> students)
        {
            _dataService.SaveAsync(students);
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
                var responsePage = new ExtendedInfoResponsePage<List<Student>>
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
