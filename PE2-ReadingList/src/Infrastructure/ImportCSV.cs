using ReadingList.Domain.Records;

namespace ReadingList.Infrastructure;

public static class ImportCSV
{
    public static async Task<List<Book>> ImportBooksAsync(string path)
    {
        var books = new List<Book>();
        using var reader = new StreamReader(path);
        string? line;
        bool first = true;

        while((line = await reader.ReadLineAsync()) != null)
        {
            if (first) { first = false; continue; }
            var parts = line.Split(',');
            if (parts.Length < 0) continue;

            try
            {
                books.Add(new Book(
               Id: int.Parse(parts[0]),
               Title: parts[1],
               Author: parts[2],
               Year: int.Parse(parts[3]),
               Pages: int.Parse(parts[4]),
               Genre: parts[5],
               Finished: parts[6] == "yes" ? true : false,
               Rating: double.Parse(parts[7])
            ));
            } catch { continue; }
        }
        return books;
    }
}
