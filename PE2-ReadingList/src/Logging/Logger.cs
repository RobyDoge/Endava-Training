using ReadingList.ExportStrategies;

namespace ReadingList.Logging;

public static class Logger
{
    private static StreamWriter Writer { get; set; }
    public static void SetWriter(string path)
    {
        using (File.Create(path)) { }
        Writer = new StreamWriter(path, append: true)
        { AutoFlush = true};

    }
    public static void Log(LogType logType, string message)
    {
        string line = $"[{DateTime.Now:dd-MM-yy HH:mm:ss}] {logType} => {message}";
        Writer.WriteLine(line);
    }

}
