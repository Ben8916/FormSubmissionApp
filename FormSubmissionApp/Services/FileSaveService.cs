using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FormSubmissionApp.Services
{
    public class FileSaveService : IFileSaveService
    {
        public async Task SaveToFileAsync<T>(string filePath, T data)
        {
            string jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, jsonData);
        }
    }
}

