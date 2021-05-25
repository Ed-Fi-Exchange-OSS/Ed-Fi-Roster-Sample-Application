﻿using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Models.EnrollmentComposites;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EdFi.Roster.Sdk.Api.EnrollmentComposites;
using EdFi.Roster.Sdk.Client;
using EdFi.Roster.Services.ApiSdk;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public class StudentService
    {
        private readonly IRosterDataService _dataService;
        private readonly IResponseHandleService _responseHandleService;
        private readonly IApiFacade _apiFacade;

        public StudentService(IRosterDataService dataService
            , IResponseHandleService responseHandleService
            , IApiFacade apiFacade)
        {
            _dataService = dataService;
            _responseHandleService = responseHandleService;
            _apiFacade = apiFacade;
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
            var api = await _apiFacade.GetApiClassInstance<StudentsApi>();
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
                    currentApiResponse = await api.GetStudentsWithHttpInfoAsync(offset, limit);
                }
                catch (ApiException exception)
                {
                    errorMessage = exception.Message;
                    if (exception.ErrorCode.Equals((int)HttpStatusCode.Unauthorized))
                    {
                        api = await _apiFacade.GetApiClassInstance<StudentsApi>(true);
                        currentApiResponse = await api.GetStudentsWithHttpInfoAsync(offset, limit);
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
