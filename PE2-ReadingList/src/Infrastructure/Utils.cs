namespace ReadingList.Infrastructure;

public static class Utils
{
    public static string? GetFullPath(string folderLocation, string file)
    {
        string fullPath = $"{folderLocation}/{file}";
        if (File.Exists(fullPath)) { return fullPath; }
        return null;
    }
}
