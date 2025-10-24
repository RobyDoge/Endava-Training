namespace ReadingList.ExportStrategies;

public interface IExportStrategy
{
    Task SaveAsync<T>(ICollection<T> collection, string filepath);
}
