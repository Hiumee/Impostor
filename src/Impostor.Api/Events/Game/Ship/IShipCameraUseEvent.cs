namespace Impostor.Api.Events.Ship
{
    public interface IShipCameraUseEvent : IShipEvent
    {
        // TODO: Change the door id with an enum(?)
        uint NumberOfUsers { get; }
    }
}
