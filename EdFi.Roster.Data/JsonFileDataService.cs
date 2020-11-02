using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EdFi.Roster.Data
{
    public class CrossWalk
    {
        public string FileId { get; set; }
        public string TypeFullName { get; set; }
    }

    public interface IDataService
    {
        Task<int> SaveAsync<T>(T dataIn);
        Task<T> ReadAsync<T>() where T : new();
    }

    public class JsonFileDataService : IDataService
    {
        private readonly string _filePath;
        public JsonFileDataService(string filePath = null)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName;
            Debug.WriteLine($"\"projectDirectory\" value - {projectDirectory}.");
            _filePath = Path.Combine(projectDirectory, "EdFi.Roster.Data", "JsonDataFiles");

            if (!string.IsNullOrEmpty(filePath))
                _filePath = filePath;
        }

        public async Task<int> SaveAsync<T>(T dataIn)
        {
            string rootPath = _filePath;
            string fileName = GetFileName(typeof(T));
            string fullPath = Path.Combine(rootPath, fileName) + ".json";

            //recreate on every save.....
            //May need to change for the other data
            using (FileStream fs = File.Create(fullPath))
            {
                await JsonSerializer.SerializeAsync(fs, dataIn);
            }

            return 0;
        }

        public async Task<T> ReadAsync<T>() where T : new()
        {
            string rootPath = _filePath; //_env.ContentRootPath;
            string fileName = GetFileName(typeof(T));
            string fullPath = Path.Combine(rootPath, fileName) + ".json";
            T response = new T();

            if (File.Exists(fullPath))
            {
                using (FileStream fs = File.OpenRead(fullPath))
                {
                    response = await JsonSerializer.DeserializeAsync<T>(fs);
                }
            }

            return response;
        }

        private string GetFileName(Type t)
        {
            //return t.FullName.GetHashCode().ToString();
            string rootPath = _filePath;
            string fullPath = Path.Combine(rootPath, "crosswalk") + ".json";
            List<CrossWalk> xWalk = new List<CrossWalk>();
            string returnFileName;

            if (File.Exists(fullPath))
            {
                xWalk = JsonSerializer.Deserialize<List<CrossWalk>>(File.ReadAllText(fullPath));
            }

            if (xWalk.Exists(x => x.TypeFullName == t.FullName))
            {
                return xWalk.Single(x => x.TypeFullName == t.FullName).FileId;
            }

            //none found add one
            returnFileName = Guid.NewGuid().ToString();
            xWalk.Add(new CrossWalk { FileId = returnFileName, TypeFullName = t.FullName });

            //save data
            File.WriteAllText(fullPath, JsonSerializer.Serialize(xWalk));

            return returnFileName;
        }
    }
}