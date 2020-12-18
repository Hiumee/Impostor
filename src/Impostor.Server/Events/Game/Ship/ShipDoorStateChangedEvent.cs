using System.Collections.Generic;
using Impostor.Api.Events.Ship;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;

namespace Impostor.Server.Events.Ship
{
    public class ShipDoorStateChangedEvent : IShipDoorStateChangedEvent
    {
        public ShipDoorStateChangedEvent(IGame game, IInnerShipStatus ship, ICollection<KeyValuePair<int,bool>> doors)
        {
            Game = game;
            ShipStatus = ship;
            Doors = doors;
        }

        public IGame Game { get; }

        public IInnerShipStatus ShipStatus { get; }

        public ICollection<KeyValuePair<int,bool>> Doors { get; }
    }
}
