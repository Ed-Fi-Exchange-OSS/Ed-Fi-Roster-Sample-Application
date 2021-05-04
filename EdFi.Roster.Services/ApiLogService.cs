using EdFi.Roster.Sdk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdFi.Roster.Services
{
    public class ApiLogService
    {
        private readonly Sdk.Services.ApiLogService apiLogService;
        public ApiLogService()
        {
            apiLogService = new Sdk.Services.ApiLogService();
        }
        
        public async Task<IEnumerable<ApiLogEntry>> ReadAll()
        {
            var apiLogEntries = await apiLogService.ReadAllLogsAsync();
            return apiLogEntries;
        }

        public void ClearLogs()
        {
            apiLogService.ClearLogs();
        }
    }
}
