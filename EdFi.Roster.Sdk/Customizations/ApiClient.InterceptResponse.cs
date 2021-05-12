using EdFi.Roster.Sdk.Services;
using RestSharp;

namespace EdFi.Roster.Sdk.Client
{
    public partial class ApiClient
    {
        /// <summary>
        /// Log the API call
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        partial void InterceptResponse(IRestRequest request, IRestResponse response)
        {
            var apiLogService = new ApiLogService();
            apiLogService.WriteLog(RestClient, (RestRequest)request, (RestResponse)response);
        }
    }
}
