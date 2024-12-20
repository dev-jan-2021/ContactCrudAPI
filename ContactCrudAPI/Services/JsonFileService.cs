using System.Text.Json;
using ContactCrudAPI.Models;

namespace ContactCrudAPI.Services
{
    public class JsonFileService
    {
        private readonly string _filePath;

        public JsonFileService(IWebHostEnvironment environment)
        {
            _filePath = Path.Combine(environment.ContentRootPath, "MockData", "contacts.json");
        }

        public List<Contact> GetAllContacts()
        {
            if (!File.Exists(_filePath)) return new List<Contact>();
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
            catch (JsonException ex)
            {
                // Handle any potential errors when deserializing
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<Contact>();
            }
        }

        public void SaveContacts(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
