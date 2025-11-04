namespace ReadingList.Logging;

public static class Logger
{
    private static StreamWriter? Writer { get; set; }
    public static void SetWriter(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using (File.Create(path)) { }
        Writer = new StreamWriter(path, append: true)
        { AutoFlush = true};

    }
    public static void Log(LogType logType, string message)
    {
        if (Writer is null)
            throw new InvalidOperationException("Logger writer not initialized. Call Logger.SetWriter first.");
        
        string line = $"[{DateTime.Now:dd-MM-yy HH:mm:ss}] {logType} => {message}";
        Writer.WriteLine(line);
    }
    public static void Close()
    {
        Writer?.Flush();
        Writer?.Dispose();
        Writer = null;
    }

}
