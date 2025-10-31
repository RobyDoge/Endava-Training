
using ReadingList.Domain.Records;
using System.Globalization;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

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
                var finished = b.Finished ? "Yes" : "No";

                var line = string.Join(",",
                    b.Id.ToString(CultureInfo.InvariantCulture),
                    b.Title,
                    b.Author,
                    b.Year.ToString(CultureInfo.InvariantCulture),
                    b.Pages.ToString(CultureInfo.InvariantCulture),
                    b.Genre,
                    finished,
                    b.Rating.ToString("F2", CultureInfo.InvariantCulture)
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
