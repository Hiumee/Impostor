using System;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;
using Impostor.Server.Events.Ship;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class HudOverrideSystemType : ISystemType, IActivatable
    {
        public bool IsActive { get; private set; }

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(IMessageReader reader, bool initialState)
        {
            IsActive = reader.ReadBoolean();
        }

        public async ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus ship, IEventManager eventManager)
        {
            Deserialize(reader, initialState);

            await eventManager.CallAsync(new ShipCommsStateChangedEvent(game, ship, IsActive));
        }
    }
}
