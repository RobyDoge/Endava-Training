namespace AirportTool.Application.Models;

public class QuerryParameters
{
    public int PageSize { get; }

    public int PageNumber { get; set; }
    public AppSettings AppSettings { get; }

    public QuerryParameters(AppSettings appSettings)
    {
        AppSettings = appSettings;
        PageSize = AppSettings.PageSize;
    }
}