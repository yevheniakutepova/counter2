using System.Text.Json;

namespace Counter.Models
{
    public class CounterService
    {
        private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.json");

        public List<CounterModel> LoadCounters()
        {
            if (!File.Exists(filePath))
                return new List<CounterModel>();

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<CounterModel>>(json) ?? new List<CounterModel>();
        }

        public void SaveCounters(List<CounterModel> counters)
        {
            string json = JsonSerializer.Serialize(counters);
            File.WriteAllText(filePath, json);
        }
    }
}

