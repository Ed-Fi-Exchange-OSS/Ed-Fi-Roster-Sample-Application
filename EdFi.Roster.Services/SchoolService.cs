﻿using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdFi.Roster.Services.ApiSdk;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class SchoolService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;

        public SchoolService(IRosterDataService rosterDataService
                            , IConfigurationService configurationService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
        }

        public async Task Save(List<School> schools)
        {
            var schoolList = schools.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterSchool {Content = content}).ToList();

            await _rosterDataService.SaveAsync(schoolList);
        }

        public async Task<IEnumerable<School>> ReadAllAsync()
        {
            var schools = await _rosterDataService.ReadAllAsync<RosterSchool>();
            return schools.Select(school => JsonConvert.DeserializeObject<School>(school.Content)).ToList();
        }

        public async Task<ExtendedInfoResponse<List<School>>> GetAllSchoolsWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var api = new ApiFacade(apiConfiguration).SchoolsApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<School>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await api.GetSchoolsWithHttpInfoAsync(offset, limit);
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
