using System;
using System.Collections.Generic;
using Impostor.Api.Events.Ship;
using Impostor.Api.Games;
using Impostor.Api.Net;
using Impostor.Api.Net.Inner.Objects;

namespace Impostor.Server.Events.Ship
{
    public class ShipReactorStateChangedEvent : IShipReactorStateChangedEvent
    {
        public ShipReactorStateChangedEvent(IGame game, IInnerShipStatus ship, bool isActive, float countdown, ICollection<Tuple<IClientPlayer, byte>> playersHolding)
        {
            Game = game;
            ShipStatus = ship;
            IsActive = isActive;
            Countdown = countdown;
            PlayersHolding = playersHolding;
        }

        public IGame Game { get; }

        public IInnerShipStatus ShipStatus { get; }

        public bool IsActive { get; }

        public float Countdown { get; }

        public ICollection<Tuple<IClientPlayer, byte>> PlayersHolding { get; }
    }
}
