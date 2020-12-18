using System.Collections.Generic;

namespace Impostor.Api.Events.Ship
{
    public interface IShipDoorStateChangedEvent : IShipEvent
    {
        // TODO: Change the door id with an enum(?)
        ICollection<KeyValuePair<int,bool>> Doors { get; }
    }
}
