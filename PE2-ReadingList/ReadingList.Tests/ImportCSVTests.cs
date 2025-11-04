using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using ReadingList.Logging;
using System.Text;

namespace ReadingList.Tests;

public class ImportCSVTests
{
    [Fact]
    public async Task AddEntry_ValidLine()
    {
        var csvPath = Path.GetTempFileName();
        File.WriteAllLines(csvPath, new[]
        {
        "Id,Title,Author,Year,Pages,Genre,Finished,Rating",
        "11,Pixels and Glory,Hannah Suzuki,2021,342,ESports,Yes,4.30",
        });
        var result = await ImportCSV.ImportBooksAsync(csvPath);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddEntry_MalformedLine()
    {
        var csvPath = Path.GetTempFileName();
        var logPath = Path.GetTempFileName();

        Logger.SetWriter(logPath);
        File.WriteAllLines(csvPath,
        [
        "Id,Title,Author,Year,Pages,Genre,Finished,Rating",
        "11,Pixels and Glory,Hannah Suzuki,2021,",
        ]);
        var result = await ImportCSV.ImportBooksAsync(csvPath);

        Assert.True(result.IsFailure);
        Assert.Equal(Error.NullValue, result.Error);
    }

}
