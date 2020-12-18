using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class MedScanSystem : ISystemType
    {
        public MedScanSystem()
        {
            UsersList = new List<byte>();
        }

        public List<byte> UsersList { get; }

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public void Deserialize(IMessageReader reader, bool initialState)
        {
            UsersList.Clear();

            var num = reader.ReadPackedInt32();

            for (var i = 0; i < num; i++)
            {
                UsersList.Add(reader.ReadByte());
            }
        }

        public async ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus ship, IEventManager eventManager)
        {
            Deserialize(reader, initialState);

            List<IClientPlayer> players = new List<IClientPlayer>();

            foreach (var id in UsersList)
            {
                players.Add(game.Players.Where(x => x.Character.PlayerId == id).First());
            }
        }
    }
}
