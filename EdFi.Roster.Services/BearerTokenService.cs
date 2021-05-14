using System;
using System.Linq;
using System.Net;
using EdFi.Roster.Models;
using EdFi.Roster.Sdk.Client;
using EdFi.Roster.Sdk.Services;
using RestSharp;

namespace EdFi.Roster.Services
{
    public class BearerTokenService
    {
        private ApiSettings apiSettings;
        private readonly IDataService dataService;
        public BearerTokenService()
        {
            apiSettings = new ApiSettingsService().Read();
            dataService = new JsonDataFileService();
        }

        public string GetNewBearerToken()
        {
            return GetNewBearerTokenResponse().Data.AccessToken;
        }
        public ApiResponse<BearerTokenResponse> GetNewBearerTokenResponse(string url, string key, string secret)
        {
            this.apiSettings = new ApiSettings
            {
                RootUrl = url,
                Key = key,
                Secret = secret
            };
            return GetNewBearerTokenResponse();
        }

        public ApiResponse<BearerTokenResponse> GetNewBearerTokenResponse()
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
            new ApiLogService().WriteLog(oauthClient, bearerTokenRequest, bearerTokenResponse);

            //save token info
            dataService.Save(new BearerTokenInformation
            {
                DateTimeCreated = DateTime.Now,
                ExpiresIn = bearerTokenResponse.Data.ExpiresIn,
                AccessToken = bearerTokenResponse.Data.AccessToken
            });

            //return results
            return new ApiResponse<BearerTokenResponse>((int)bearerTokenResponse.StatusCode,
                bearerTokenResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (BearerTokenResponse)bearerTokenResponse.Data,
                bearerTokenResponse.ResponseUri);
        }
        public BearerTokenInformation GetBearerTokenInformation()
        {
            //check for existing and return if exists
            var bearerTokenInfo = dataService.Read<BearerTokenInformation>();
            if (!string.IsNullOrEmpty(bearerTokenInfo.AccessToken))
                return bearerTokenInfo;

            //if here, no access token existed
            //GetBearerToken new if no existing
            GetNewBearerTokenResponse();
            return dataService.Read<BearerTokenInformation>();
        }

        public string GetBearerToken()
        {
            var tokenInfo = GetBearerTokenInformation();
            var accessToken = tokenInfo.AccessToken;
            if (tokenInfo.DateTimeCreated.AddSeconds(tokenInfo.ExpiresIn - 120).CompareTo(DateTime.Now) < 0)
            {
                //get new token
                accessToken = GetNewBearerToken();
            }

            return accessToken;
        }
    }
}
