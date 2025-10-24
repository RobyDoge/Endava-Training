namespace ReadingList.ExportStrategies;

public class JsonExportStrategy : IExportStrategy
{
    public async Task SaveAsync<T>(ICollection<T> collection, string filepath)
    {
        throw new NotImplementedException();
    }
}
