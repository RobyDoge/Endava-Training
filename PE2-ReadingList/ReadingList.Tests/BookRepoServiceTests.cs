using ReadingList.Domain;
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

    //[Fact]
    //public async Task 
}
