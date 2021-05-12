using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class SectionService
    {
        private readonly IRosterDataService _rosterDataService;

        public SectionService(IRosterDataService rosterDataService)
        {
            _rosterDataService = rosterDataService;
        }

        public async Task Save(List<Section> sections)
        {
            var sectionsList = sections.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterSection { Content = content }).ToList();
            await _rosterDataService.SaveAsync(sectionsList);
        }

        public async Task<IEnumerable<Section>> ReadAllAsync()
        {
            var sections = await _rosterDataService.ReadAllAsync<RosterSection>();
            return sections.Select(section => JsonConvert.DeserializeObject<Section>(section.Content)).ToList();
        }

        public async Task<ExtendedInfoResponse<List<Section>>> GetAllSectionsWithExtendedInfoAsync()
        {
            var sectionsApi = new ApiSdk.ApiFacade().SectionsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<Section>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await sectionsApi.GetSectionsAsyncWithHttpInfo(offset, limit);
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
