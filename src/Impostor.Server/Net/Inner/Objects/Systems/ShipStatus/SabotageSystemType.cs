using System;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class SabotageSystemType : ISystemType
    {
        private readonly IActivatable[] _specials;

        public SabotageSystemType(IActivatable[] specials)
        {
            _specials = specials;
        }

        public float Timer { get; set; }

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(IMessageReader reader, bool initialState)
        {
            Timer = reader.ReadSingle();
        }

        public async ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus ship, IEventManager eventManager)
        {
            Deserialize(reader, initialState);
        }
    }
}
