using System.Threading.Tasks;
using Impostor.Api.Events.Managers;
using Impostor.Api.Games;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Messages;

namespace Impostor.Server.Net.Inner.Objects.Systems
{
    public interface ISystemType
    {
        void Serialize(IMessageWriter writer, bool initialState);

        void Deserialize(IMessageReader reader, bool initialState);

        // TODO: Modify the interface for InnerShipStatus - passing it as it could be relevant information
        ValueTask HandleDataAsync(IMessageReader reader, bool initialState, IGame game, IInnerShipStatus innerShipStatus, IEventManager eventManager);
    }
}
