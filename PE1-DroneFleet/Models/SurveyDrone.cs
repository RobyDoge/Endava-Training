using DroneFleet.Models.Interfaces;

namespace DroneFleet.Models;

internal class SurveyDrone : Drone, INavigable, IPhotoCapture
{

    public int PhotoCount { get; private set; } = 0;
    public (double latitude, double longitude)? CurrentWaypoint { get; private set; } = null;

    public SurveyDrone(int id, int name, decimal batteryPercent) : base(id, name, batteryPercent)
    {
    }
    public void SetWaypoint(double latitude, double longitude)
    {
        CurrentWaypoint = (latitude, longitude);
    }

    public void TakePhoto()
    {
        PhotoCount++;
    }
}
