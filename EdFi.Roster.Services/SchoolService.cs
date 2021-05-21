﻿using EdFi.Roster.Models;
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
    public class SchoolService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;
        private readonly IResponseHandleService _responseHandleService;

        public SchoolService(IRosterDataService rosterDataService
                            , IConfigurationService configurationService
                            , IResponseHandleService responseHandleService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
            _responseHandleService = responseHandleService;
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
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<School>> currentApiResponse = null;
                try
                {
                    currentApiResponse = await api.GetSchoolsWithHttpInfoAsync(offset, limit); 
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                        apiConfiguration = await _configurationService.ApiConfiguration(true);
                        api = new ApiFacade(apiConfiguration).SchoolsApi;
                        currentApiResponse = await api.GetSchoolsWithHttpInfoAsync(offset, limit);
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
