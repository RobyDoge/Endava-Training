using ReadingList.App;
using ReadingList.Domain.Records;
using ReadingList.Logging;

string dataFolderPath = Path.GetFullPath("../../../../data");
Func<Book,int> keySelector = (book) => book.Id;
var menu = new Menu(dataFolderPath, keySelector);

string logPath = Path.GetFullPath("../../../logs");
string logName = $"log-[{DateTime.Now:yyyy-MM-dd_HH-mm-ss}].log";
Logger.SetWriter($"{logPath}/{logName}");

await menu.Run();

Logger.Close();