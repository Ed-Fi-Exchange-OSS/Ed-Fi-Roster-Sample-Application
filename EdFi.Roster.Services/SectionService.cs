using EdFi.Roster.Data;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class SectionService
    {
        private readonly IDataService _dataService;

        public SectionService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Save(List<Section> sections)
        {
            _dataService.SaveAsync(sections);
        }

        public IEnumerable<Section> ReadAll()
        {
            return _dataService.ReadAsync<List<Section>>().Result;
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
                var responsePage = new ExtendedInfoResponsePage<List<Section>>
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
