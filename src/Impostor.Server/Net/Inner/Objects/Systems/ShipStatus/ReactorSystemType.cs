using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;
using Impostor.Server.Events.Ship;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class ReactorSystemType : ISystemType, IActivatable
    {
        public ReactorSystemType()
        {
            Countdown = 10000f;
            UserConsolePairs = new HashSet<Tuple<byte, byte>>();
        }

        public float Countdown { get; private set; }

        public HashSet<Tuple<byte, byte>> UserConsolePairs { get; }

        public bool IsActive => Countdown < 10000.0;

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(IMessageReader reader, bool initialState)
        {
            Countdown = reader.ReadSingle();
            UserConsolePairs.Clear(); // TODO: Thread safety

            var count = reader.ReadPackedInt32();

            for (var i = 0; i < count; i++)
            {
                UserConsolePairs.Add(new Tuple<byte, byte>(reader.ReadByte(), reader.ReadByte()));
            }
        }

        public async ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus ship, IEventManager eventManager)
        {
            Deserialize(reader, initialState);

            var playersHolding = new HashSet<Tuple<IClientPlayer, byte>>();
            foreach (var pair in UserConsolePairs)
            {
                playersHolding.Add(new Tuple<IClientPlayer, byte>(game.Players.Where(x => x.Character.PlayerId == pair.Item1).First(), pair.Item2));
            }

            await eventManager.CallAsync(new ShipReactorStateChangedEvent(game, ship, IsActive, Countdown, playersHolding));
        }
    }
}
