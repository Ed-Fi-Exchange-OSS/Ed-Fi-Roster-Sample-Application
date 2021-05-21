﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdFi.Roster.Models;

namespace EdFi.Roster.Services
{
    public class ApiSettingsService
    {
        private readonly IRosterDataService _dataService;

        public ApiSettingsService(IRosterDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task Save(ApiSettings apiSettings)
        {
           await _dataService.SaveAsync(new List<ApiSettings>
           {
               apiSettings
           });
        }

        public async Task<ApiSettings> Read()
        {
            var result = await _dataService.ReadAllAsync<ApiSettings>();
            var apiSetting = result.ToList().FirstOrDefault();
            return new ApiSettings { Key = apiSetting?.Key, RootUrl = apiSetting?.RootUrl, Secret = apiSetting?.Secret };
        }
    }
}
