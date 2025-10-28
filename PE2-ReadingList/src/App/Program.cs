using ReadingList.App;

string dataFolderPath = Path.GetFullPath("../../../../data");
var menu = new Menu(dataFolderPath);
menu.Run();