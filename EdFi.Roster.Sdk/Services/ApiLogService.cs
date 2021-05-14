using EdFi.Roster.Sdk.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace EdFi.Roster.Sdk.Services
{
    public class ApiLogService
    {
        private readonly string fullFilePath;

        public ApiLogService()
        {
            var filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "EdFi.Roster.Sdk", "Data");
            this.fullFilePath = Path.Combine(filePath, typeof(List<ApiLogEntry>).FullName) + ".json";
        }

        public void WriteLog(IRestClient client, IRestRequest request, IRestResponse response)
        {
            var apiLogEntry = new ApiLogEntry
            {
                LogDateTime = DateTime.Now,
                Client = new ClientLog
                {
                    BaseUrl = client.BaseUrl.OriginalString,
                    FullUrl = client.BuildUri(request).OriginalString
                },
                Request = new RequestLog
                {

                    Resource = request.Resource,
                    // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                    // otherwise it will just show the enum value
                    Parameters = request.Parameters.Select(parameter => new ApiCallLogParameter
                    {
                        Name = parameter.Name,
                        Value = parameter.Value,
                        Type = parameter.Type.ToString()
                    }).ToList(),
                    // ToString() here to have the method as a nice string otherwise it will just show the enum value
                    Method = request.Method.ToString(),
                    // This will generate the actual Uri used in the request

                    Uri = client.BuildUri(request),
                },
                Response = new ResponseLog
                {
                    StatusCode = response.StatusCode,
                    Content = response.Content,
                    Headers = response.Headers.Select(parameter => new ApiCallLogParameter
                    {
                        Name = parameter.Name,
                        Value = parameter.Value,
                        Type = parameter.Type.ToString()
                    }).ToList(),
                    // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                    Uri = response.ResponseUri,
                    ErrorMessage = response.ErrorMessage,
                }
            };
            //var logData = dataService.Read<List<ApiLogEntry>>();
            var logData = new List<ApiLogEntry>
                                {
                                    apiLogEntry
                                };
            WriteApiLog(apiLogEntry);
        }

        static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private void WriteApiLog(ApiLogEntry apiLog)
        {
            //read it all

            var logEntries = new List<ApiLogEntry>();

            try
            {
                locker.EnterWriteLock();
                if (File.Exists(fullFilePath))
                {
                    logEntries = JsonSerializer.Deserialize<List<ApiLogEntry>>(File.ReadAllText(fullFilePath));
                }
                logEntries.Add(apiLog);
                File.WriteAllText(fullFilePath, JsonSerializer.Serialize(logEntries));
            }
            finally
            {
                locker.ExitWriteLock();
            }

        }

        public void ClearLogs()
        {
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }
        }

        public async Task<List<ApiLogEntry>> ReadAllLogsAsync()
        {

            var apiLogs = new List<ApiLogEntry>();
            if (File.Exists(fullFilePath))
            {
                using (FileStream fs = File.OpenRead(fullFilePath))
                {
                    apiLogs = await JsonSerializer.DeserializeAsync<List<ApiLogEntry>>(fs);
                }
            }
            return apiLogs;
        }

    }

    //public sealed class JsonApiLogWriter:IDisposable
    //{
    //    private static FileStream fileStream;
    //    public void WriteLog(List<ApiLogEntry> existingLogs, ApiLogEntry newEntry)
    //    {
    //        existingLogs.Add(newEntry);
    //        using (StreamWriter sr = new StreamWriter(fileStream))
    //        {
    //            // File writing as usual
    //            sr.Write(JsonConvert.SerializeObject(existingLogs));
    //        }
    //    }

    //    private JsonApiLogWriter()
    //    {
    //        var filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "EdFi.Roster.Sdk", "Data");
    //        string fullPath = Path.Combine(filePath, typeof(List<ApiLogEntry>).FullName) + ".json";
    //        fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
    //    }
    //    public static JsonApiLogWriter Instance { get; } = new JsonApiLogWriter();

    //    public void Dispose()
    //    {
    //        fileStream.Close();
    //    }
    //}
}
