using EdFi.Roster.Sdk.Models;
using System;
using System.Collections.Generic;

namespace EdFi.Roster.Services
{
    public class ApiLogService
    {
        private readonly Sdk.Services.ApiLogService apiLogService;
        public ApiLogService()
        {
            apiLogService = new Sdk.Services.ApiLogService();
        }
        
        public IEnumerable<ApiLogEntry> ReadAll()
        {
            return apiLogService.ReadAllLogsAsync().Result;
        }

        public void ClearLogs()
        {
            apiLogService.ClearLogs();
        }
    }
}
