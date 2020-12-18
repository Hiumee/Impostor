using Impostor.Api.Events.Ship;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;

namespace Impostor.Server.Events.Ship
{
    public class ShipCommsStateChangedEvent : IShipCommsStateChangedEvent
    {
        public ShipCommsStateChangedEvent(IGame game, IInnerShipStatus ship, bool active)
        {
            Game = game;
            ShipStatus = ship;
            Active = active;
        }

        public IGame Game { get; }

        public IInnerShipStatus ShipStatus { get; }

        public bool Active { get; }
    }
}
