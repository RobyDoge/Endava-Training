using ReadingList.Domain.Records;
using System.Text.Json;

namespace ReadingList.ExportStrategies;

public class JsonExportStrategy : IExportStrategy
{
    public async Task<bool> SaveAsync(IEnumerable<Book> collection, string filepath)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(collection, options);
            await File.WriteAllTextAsync(filepath, json);

            return true;
        }
        catch
        {
            return false;
        }
    }
}