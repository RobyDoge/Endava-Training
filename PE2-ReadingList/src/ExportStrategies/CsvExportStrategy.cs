using ReadingList.Domain.Records;
using System.Text;

namespace ReadingList.ExportStrategies;

public class CsvExportStrategy : IExportStrategy
{
    public async Task<bool> SaveAsync(IEnumerable<Book> collection, string filepath)
    {
        try
        {
            using var writer = new StreamWriter(filepath, false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));

            await writer.WriteLineAsync("Id,Title,Author,Year,Pages,Genre,Finished,Rating");

            foreach (var b in collection)
            {
                var finished = b.Finished ? "yes" : "no";

                var line = string.Join(",",
                    b.Id.ToString(),
                    b.Title,
                    b.Author,
                    b.Year.ToString(),
                    b.Pages.ToString(),
                    b.Genre,
                    finished,
                    b.Rating.ToString("F2")
                );

                await writer.WriteLineAsync(line);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}