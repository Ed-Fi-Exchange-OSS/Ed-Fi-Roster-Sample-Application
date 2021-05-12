using System;
using EdFi.Roster.Sdk.Services;
using RestSharp;

namespace EdFi.Roster.Sdk.Client
{
    public partial class ApiClient
    {
        /// <summary>
        /// Allows for extending request processing for <see cref="T:EdFi.Roster.Sdk.Client.ApiClient" /> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        partial void InterceptRequest(IRestRequest request)
        {

            if (String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                var tokenService = new BearerTokenService();
                var tokenInfo = tokenService.GetBearerTokenInformation();
                var accessToken = tokenInfo.AccessToken;
                if (tokenInfo.DateTimeCreated.AddSeconds(tokenInfo.ExpiresIn - 120).CompareTo(DateTime.Now) < 0)
                {
                    //get new token
                    accessToken = tokenService.GetNewBearerToken();
                }
                var configuration = new Configuration { AccessToken = accessToken };
                GlobalConfiguration.Default = configuration;
                request.AddHeader("Authorization", "Bearer " + accessToken);
            }

        }
    }

}
