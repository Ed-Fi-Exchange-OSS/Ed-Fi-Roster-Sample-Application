﻿using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class LocalEducationAgencyService
    {
        private readonly IRosterDataService _rosterDataService;

        public LocalEducationAgencyService(IRosterDataService rosterDataService)
        {
            _rosterDataService = rosterDataService;
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
            var leaApi = new ApiSdk.ApiFacade().LocalEducationAgenciesApi;
            var limit = 100;
            var offset = 0;
            var response = new ExtendedInfoResponse<List<LocalEducationAgency>>();
            int currResponseRecordCount;

            do
            {
                var currResponse = await leaApi.GetLocalEducationAgenciesWithHttpInfoAsync(offset, limit);
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

