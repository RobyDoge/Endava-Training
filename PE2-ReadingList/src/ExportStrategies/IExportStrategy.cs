using ReadingList.Domain.Records;

namespace ReadingList.ExportStrategies;

public interface IExportStrategy
{
    Task<bool> SaveAsync(IEnumerable<Book> collection, string filepath);
}
