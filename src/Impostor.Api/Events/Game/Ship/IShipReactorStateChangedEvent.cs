using System;
using System.Collections.Generic;
using Impostor.Api.Net;

namespace Impostor.Api.Events.Ship
{
    public interface IShipReactorStateChangedEvent : IShipEvent
    {
        public bool IsActive { get; }

        public float Countdown { get; }

        public ICollection<Tuple<IClientPlayer, byte>> PlayersHolding { get; }
    }
}
