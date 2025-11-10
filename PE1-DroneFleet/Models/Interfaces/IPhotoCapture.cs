namespace DroneFleet.Models.Interfaces;

internal interface IPhotoCapture
{
    int PhotoCount { get; }

    void TakePhoto();
}