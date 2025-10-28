using ReadingList.App;
using ReadingList.Domain.Records;

string dataFolderPath = Path.GetFullPath("../../../../data");
Func<Book,int> keySelector = (book) => book.Id;
var menu = new Menu(dataFolderPath, keySelector);
await menu.Run();