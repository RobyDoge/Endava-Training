namespace ReadingList.Domain.Records;

public record Book(
    int Id,
    string Title,
    string Author,
    int Year,
    int Pages,
    string Genre,
    bool Finished,
    double Rating
);