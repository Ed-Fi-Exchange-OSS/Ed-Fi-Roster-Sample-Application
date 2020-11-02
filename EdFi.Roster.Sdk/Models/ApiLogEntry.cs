using EdFi.Roster.Sdk.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EdFi.Roster.Sdk.Models
{
    public class ApiLogEntry
    {
        public DateTime LogDateTime { get; set; }
        public ClientLog Client { get; set; }
        public RequestLog Request { get; set; }
        public ResponseLog Response { get; set; }

    }

    public class ClientLog
    {
        public string BaseUrl { get; set; }
        public string FullUrl { get; set; }
    }

    public class RequestLog
    {
        public string Resource { get; set; }
        public List<ApiCallLogParameter> Parameters { get; set; }
        public string Method { get; set; }
        public Uri Uri { get; set; }
    }

    public class ApiCallLogParameter
    {
        public string Name { get; set; }
        public Object Value { get; set; }
        public string Type { get; set; }
    }

    public class ResponseLog
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
        public List<ApiCallLogParameter> Headers { get; set; }
        public Uri Uri { get; set; }
        public string ErrorMessage { get; set; }
    }
}
