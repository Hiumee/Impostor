using System.Collections.Generic;
using Impostor.Api.Events.Ship;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;

namespace Impostor.Server.Events.Ship
{
    public class ShipLightsEvent : IShipLightsEvent
    {
        public ShipLightsEvent(IGame game, IInnerShipStatus ship, IEnumerable<bool> lights)
        {
            Game = game;
            ShipStatus = ship;
            Lights = lights;
        }

        public IGame Game { get; }

        public IInnerShipStatus ShipStatus { get; }

        public IEnumerable<bool> Lights { get; }
    }
}
