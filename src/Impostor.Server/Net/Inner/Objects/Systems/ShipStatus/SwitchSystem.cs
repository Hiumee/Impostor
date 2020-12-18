using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;
using Impostor.Server.Events.Ship;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class SwitchSystem : ISystemType, IActivatable
    {
        public byte ExpectedSwitches { get; set; }

        public byte ActualSwitches { get; set; }

        public byte Value { get; set; } = byte.MaxValue;

        public bool IsActive { get; }

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(IMessageReader reader, bool initialState)
        {
            ExpectedSwitches = reader.ReadByte();
            ActualSwitches = reader.ReadByte();
            Value = reader.ReadByte();
        }

        public async ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus ship, IEventManager eventManager)
        {
            Deserialize(reader, initialState);

            List<bool> lights = new List<bool>();

            for (int i=0; i<5; i++)
            {
                lights.Add(((ActualSwitches >> i) & 1) == ((ExpectedSwitches >> i) & 1));
            }

            await eventManager.CallAsync(new ShipLightsEvent(game, ship, lights));
        }
    }
}
