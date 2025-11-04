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
            try 
            {
                books.Add(ParseLine(line));
            } 
            catch(FormatException fe) 
            {
                Logger.Log(LogType.MalformedRow, $"{fe.Message} found on line {lineIndex}");
            }
            catch (Exception ex) 
            {
                Logger.Log(LogType.MalformedRow, $"{ex.Message} found on line {lineIndex}");
            }
        }

        if (books.Count > 0) return Result.Success(books);
        Logger.Log(LogType.MalformedRow, "Empty or invalid CSV file");
        return Result.Failure<List<Book>>(Error.NullValue);
    }
    private static Book ParseLine(string line)
    {
        const int expectedColumnCount = 8;
        var parts = line.Split(',');
        if (parts.Length != 8)
        {
            throw new FormatException($"CSV line has {parts.Length} columns, expected {expectedColumnCount}.");
        }
        return new Book(
            Id: int.Parse(parts[0]),
            Title: parts[1],
            Author: parts[2],
            Year: int.Parse(parts[3]),
            Pages: int.Parse(parts[4]),
            Genre: parts[5],
            Finished: parts[6].Trim().Equals("yes", StringComparison.OrdinalIgnoreCase),
            Rating: double.Parse(parts[7])
        );
    }
}