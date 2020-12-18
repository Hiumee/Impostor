using System.Collections.Generic;

namespace Impostor.Api.Events.Ship
{
    public interface IShipLightsEvent : IShipEvent
    {
        public IEnumerable<bool> Lights { get; }
    }
}
