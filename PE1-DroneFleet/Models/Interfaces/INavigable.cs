namespace DroneFleet.Models.Interfaces;

internal interface INavigable
{
    (double latitude, double longitude)? CurrentWaypoint { get; }
    void SetWaypoint(double latitude, double longitude);
}
