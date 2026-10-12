using AirportTool.Domain.Errors;

namespace AirportTool.Application.Records;

public class BulkImportFlightScheduleSummary
{
    public int TotalRecords { get; set; }
    public int CreatedRecords { get; set; }
    public int UpdatedRecords { get; set; }
    public List<Error> Errors { get; set; } = [];
}