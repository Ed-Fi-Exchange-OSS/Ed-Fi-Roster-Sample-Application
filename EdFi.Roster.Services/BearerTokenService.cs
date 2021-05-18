using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Client;
using RestSharp;

namespace EdFi.Roster.Services
{
    public class BearerTokenService
    {
        private readonly IRosterDataService _dataService;

        public BearerTokenService(IRosterDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<ApiResponse<BearerTokenResponse>> GetNewBearerTokenResponse(ApiSettings apiSettings)
        {
            var oauthClient = new RestClient(apiSettings.RootUrl);
            var bearerTokenRequest = new RestRequest("oauth/token", Method.POST);
            bearerTokenRequest.AddParameter("Client_id", apiSettings.Key);
            bearerTokenRequest.AddParameter("Client_secret", apiSettings.Secret);
            bearerTokenRequest.AddParameter("Grant_type", "client_credentials");

            var bearerTokenResponse = oauthClient.Execute<BearerTokenResponse>(bearerTokenRequest);

            if (!string.IsNullOrEmpty(bearerTokenResponse.Data.Error) || bearerTokenResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException((int)bearerTokenResponse.StatusCode, bearerTokenResponse.Data.Error);
            }

            //log api call
            // new ApiLogService().WriteLog(oauthClient, bearerTokenRequest, bearerTokenResponse);

            //save token info
            await _dataService.SaveAsync(new List<BearerTokenInformation>
            {
                new BearerTokenInformation
                {
                    DateTimeCreated = DateTime.Now,
                    ExpiresIn = bearerTokenResponse.Data.ExpiresIn,
                    AccessToken = bearerTokenResponse.Data.AccessToken
                }
            });

            var headersMap = new Multimap<string, string>();

            foreach (var header in bearerTokenResponse.Headers)
            {
                headersMap.Add(header.Name, header.Value.ToString());
            }

            //return results
            return new ApiResponse<BearerTokenResponse>(bearerTokenResponse.StatusCode,
                headersMap,
                (BearerTokenResponse)bearerTokenResponse.Data,
                bearerTokenResponse.ResponseUri);
        }
        
        public async Task<string> GetBearerToken(ApiSettings apiSettings)
        {
            var bearerTokens = await _dataService.ReadAllAsync<BearerTokenInformation>();
            string accessToken;
            var bearerTokenInfo = bearerTokens.FirstOrDefault();
            if (bearerTokenInfo != null && !string.IsNullOrEmpty(bearerTokenInfo.AccessToken) 
                        && bearerTokenInfo.DateTimeCreated.AddSeconds(bearerTokenInfo.ExpiresIn - 120).CompareTo(DateTime.Now) < 0)
            {
                accessToken = bearerTokenInfo.AccessToken;
            }
            else
            {
                var response = await GetNewBearerTokenResponse(apiSettings);
                accessToken = response.Data.AccessToken;
            }
            return accessToken;
        }
    }
}
