
namespace ReadingList.Domain;

internal record Book
{
    Guid ID;
    string Title;
    string Author;
    string Year;
    int Pages;
    string Genre;
    bool Finished;
    int Rating;
}

