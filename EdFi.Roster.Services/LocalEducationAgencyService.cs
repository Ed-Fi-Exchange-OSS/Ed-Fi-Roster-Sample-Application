using System;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EdFi.Roster.Sdk.Client;
using EdFi.Roster.Services.ApiSdk;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class LocalEducationAgencyService
    {
        private readonly IRosterDataService _rosterDataService;
        private readonly IConfigurationService _configurationService;
        private readonly ApiLogService _apiLogService;

        public LocalEducationAgencyService(IRosterDataService rosterDataService
                , IConfigurationService configurationService
                , ApiLogService apiLogService)
        {
            _rosterDataService = rosterDataService;
            _configurationService = configurationService;
            _apiLogService = apiLogService;
        }

        public async Task Save(List<LocalEducationAgency> localEducationAgencies)
        {
            var leas = localEducationAgencies.Select(JsonConvert.SerializeObject)
                .Select(content => new RosterLocalEducationAgency {Content = content}).ToList();

             await _rosterDataService.SaveAsync(leas);
        }

        public async Task<IEnumerable<LocalEducationAgency>> ReadAllAsync()
        {
            var leas = await _rosterDataService.ReadAllAsync<RosterLocalEducationAgency>();
            return leas.Select(lea => JsonConvert.DeserializeObject<LocalEducationAgency>(lea.Content)).ToList();
        }

        public async Task<ExtendedInfoResponse<List<LocalEducationAgency>>> GetAllLocalEducationAgenciesWithExtendedInfoAsync()
        {
            var apiConfiguration = await _configurationService.ApiConfiguration();
            var leaApi = new ApiFacade(apiConfiguration).LocalEducationAgenciesApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<LocalEducationAgency>>();
            int currResponseRecordCount = 0;

            do
            {
                var errorMessage = string.Empty;
                ApiResponse<List<LocalEducationAgency>> currentApiResponse = null;
                try
                {
                    leaApi.Configuration = new Configuration
                    {
                        BasePath = apiConfiguration.BasePath,
                        AccessToken = apiConfiguration.AccessToken
                    };
                    currentApiResponse = await leaApi.GetLocalEducationAgenciesAsyncWithHttpInfo(offset, limit);
                }
                catch (Exception exception)
                {
                    errorMessage = exception.Message;
                }

                if (currentApiResponse == null) continue;
                currResponseRecordCount = currentApiResponse.Data.Count;
                offset += limit;
                var responsePage = new ExtendedInfoResponsePage
                {
                    RecordsCount = currentApiResponse.Data.Count,
                    ResponseUri = currentApiResponse.ResponseUri
                };
                response.GeneralInfo.Pages.Add(responsePage);
                response.FullDataSet.AddRange(currentApiResponse.Data);

                // log entry
                var apiLogEntry = new ApiLogEntry
                {
                    LogDateTime = DateTime.Now,
                    Method = "GET",
                    StatusCode = currentApiResponse.StatusCode.ToString(),
                    Content = string.IsNullOrEmpty(errorMessage)
                        ? JsonConvert.SerializeObject(currentApiResponse.Data, Formatting.Indented)
                        : errorMessage,
                    Uri = currentApiResponse.ResponseUri.ToString()
                };
                await _apiLogService.WriteLog(apiLogEntry);

            } while (currResponseRecordCount >= limit);

            response.GeneralInfo.TotalRecords = response.FullDataSet.Count;
            response.GeneralInfo.ResponseData = JsonConvert.SerializeObject(response.FullDataSet, Formatting.Indented);

            return response;
        }
    }
}

