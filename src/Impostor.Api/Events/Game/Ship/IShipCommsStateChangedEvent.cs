namespace Impostor.Api.Events.Ship
{
    public interface IShipCommsStateChangedEvent : IShipEvent
    {
        bool Active { get; }
    }
}
