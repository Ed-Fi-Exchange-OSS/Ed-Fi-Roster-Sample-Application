﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Client;
using Newtonsoft.Json;

namespace EdFi.Roster.Services
{
    public interface IResponseHandleService
    {
        Task<ExtendedInfoResponse<List<T>>> Handle<T>(ApiResponse<List<T>> apiResponse, ExtendedInfoResponse<List<T>> response, string errorMessage)
            where T : class;
    }

    public class ResponseHandleService : IResponseHandleService
    {
        private readonly ApiLogService _logService;

        public ResponseHandleService(ApiLogService logService)
        {
            _logService = logService;
        }

        public async Task<ExtendedInfoResponse<List<T>>> Handle<T>(ApiResponse<List<T>> apiResponse, 
            ExtendedInfoResponse<List<T>> response, string errorMessage) where T : class
        {
            var responsePage = new ExtendedInfoResponsePage
            {
                RecordsCount = apiResponse.Data.Count,
                ResponseUri = apiResponse.ResponseUri
            };
            response.GeneralInfo.Pages.Add(responsePage);
            response.FullDataSet.AddRange(apiResponse.Data);

            // log entry
            var apiLogEntry = new ApiLogEntry
            {
                LogDateTime = DateTime.Now,
                Method = "GET",
                StatusCode = apiResponse.StatusCode.ToString(),
                Content = string.IsNullOrEmpty(errorMessage)
                    ? JsonConvert.SerializeObject(apiResponse.Data, Formatting.Indented)
                    : errorMessage,
                Uri = apiResponse.ResponseUri.ToString()
            };
            await _logService.WriteLog(apiLogEntry);

            return response;
        }
    }
}