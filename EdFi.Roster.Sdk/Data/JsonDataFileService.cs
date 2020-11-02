
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EdFi.Roster.Sdk.Data
{
    public interface IDataService
    {
        int Save<T>(T dataIn);
        T Read<T>() where T : new();
    }
    public class JsonDataFileService : IDataService
    {
        private readonly string _filePath = string.Empty;
        public JsonDataFileService(string filePath = null)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName;
            Debug.WriteLine($"\"projectDirectory\" value - {projectDirectory}.");
            _filePath = Path.Combine(projectDirectory, "EdFi.Roster.Sdk", "Data");

            if (!string.IsNullOrEmpty(filePath))
                _filePath = filePath;
        }

        public T Read<T>() where T : new()
        {
            string rootPath = _filePath;
            string fullPath = Path.Combine(rootPath, typeof(T).FullName) + ".json";
            T response = new T();

            if (File.Exists(fullPath))
            {
                response = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullPath));
            }

            return response;
        }
        private static readonly ReaderWriterLock locker = new ReaderWriterLock();
        public int Save<T>(T dataIn)
        {
            string rootPath = _filePath;
            string fullPath = Path.Combine(rootPath, typeof(T).FullName) + ".json";

            //recreate on every save.....
            //May need to change for the other data

            //File.WriteAllText(fullPath, JsonConvert.SerializeObject(dataIn));

            try
            {
                locker.AcquireWriterLock(int.MaxValue);
                using (var file = new StreamWriter(fullPath, false))
                {
                    file.Write(JsonConvert.SerializeObject(dataIn));
                }
                //System.IO.File.WriteAllText(fullPath, JsonConvert.SerializeObject(dataIn));
            }
            finally
            {
                locker.ReleaseWriterLock();
            }

            return 0;
        }
    }
}
