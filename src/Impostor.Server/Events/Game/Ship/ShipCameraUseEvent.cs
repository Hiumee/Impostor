using Impostor.Api.Events.Ship;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;

namespace Impostor.Server.Events.Ship
{
    public class ShipCameraUseEvent : IShipCameraUseEvent
    {
        public ShipCameraUseEvent(IGame game, IInnerShipStatus ship, uint numberOfUsers)
        {
            Game = game;
            ShipStatus = ship;
            NumberOfUsers = numberOfUsers;
        }

        public IGame Game { get; }

        public IInnerShipStatus ShipStatus { get; }

        public uint NumberOfUsers { get; }
    }
}
