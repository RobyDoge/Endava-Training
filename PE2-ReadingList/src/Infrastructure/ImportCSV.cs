using ReadingList.Domain;
using ReadingList.Domain.Records;
using ReadingList.Logging;

namespace ReadingList.Infrastructure;

public static class ImportCSV
{
    public static async Task<Result<List<Book>>> ImportBooksAsync(string path)
    {
        var books = new List<Book>();
        using var reader = new StreamReader(path);
        string? line;
        bool first = true;
        int lineIndex = 0;

        while((line = await reader.ReadLineAsync()) != null)
        {
            lineIndex++;
            if (first) { first = false; continue; }
            
            var parts = line.Split(',');
            if (parts.Length != 8)
            {
                Logger.Log(LogType.MalformedRow, $"Expected 8 columns, got {parts.Length} on line {lineIndex}");
                continue;
            }

            try
            {
                books.Add(new Book(
                    Id: int.Parse(parts[0]),
                    Title: parts[1],
                    Author: parts[2],
                    Year: int.Parse(parts[3]),
                    Pages: int.Parse(parts[4]),
                    Genre: parts[5],
                    Finished: parts[6].Trim().Equals("yes", StringComparison.OrdinalIgnoreCase),
                    Rating: double.Parse(parts[7])
                ));
            } catch (Exception ex) 
            {
                Logger.Log(LogType.MalformedRow, $"{ex.Message} found on line {lineIndex}");
                continue; 
            }
        }
        if (books.Count > 0) return Result.Success(books);
        
        Logger.Log(LogType.MalformedRow, "Empty or invalid CSV file");
        return Result.Failure<List<Book>>(Error.NullValue);
    }
}