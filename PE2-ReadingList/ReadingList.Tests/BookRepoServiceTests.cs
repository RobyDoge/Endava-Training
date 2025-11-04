using ReadingList.Domain;
using ReadingList.Domain.Records;
using ReadingList.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingList.Tests;

public class BookRepoServiceTests
{
    [Fact]
    public async Task GetAverage_NoBooks()
    {
        BookRepoService service = new BookRepoService(new(book => book.Id));

        var result = await service.GetAverageRatingAsync();

        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task TopRated_ExpectedOrder()
    {
        BookRepoService service = new BookRepoService(new(book => book.Id));
        var csvPath = Path.GetTempFileName();
        File.WriteAllLines(csvPath, new[]
        {
        "Id,Title,Author,Year,Pages,Genre,Finished,Rating",
        "11,Pixels and Glory,Hannah Suzuki,2021,342,ESports,Yes,4.30",
        "12,The Final Match,Leo Mart�nez,2024,276,ESports,No,3.90",
        "15,Arena Protocol,Satoshi Tan,2022,367,ESports,Yes,4.50",
        });
        await service.ImportBooksAsync(csvPath);
        var correctOrder = new List<Book>()
        {
            new Book(15,"Arena Protocol","Satoshi Tan",2022,367,"ESports",true,4.50),
            new Book(11,"Pixels and Glory","Hannah Suzuki",2021,342,"ESports",true,4.30)
        };

        var result = await service.GetTopRatedBooksAsync(correctOrder.Count);

        Assert.True(result.IsSuccess);
        Assert.Equal(correctOrder, result.Value);
    }
}
