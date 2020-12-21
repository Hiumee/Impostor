using System.Threading.Tasks;
using Impostor.Api.Net.Messages;

namespace Impostor.Server.Net.Inner.Objects.Systems.ShipStatus
{
    public class HudOverrideSystemType : ISystemType, IActivatable
    {
        public bool IsActive { get; private set; }

        public void Serialize(IMessageWriter writer, bool initialState)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask Deserialize(IMessageReader reader, bool initialState)
        {
            IsActive = reader.ReadBoolean();

            return default;
        }
    }
}
