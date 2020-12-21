using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Impostor.Api;
using Impostor.Api.Events.Managers;
using Impostor.Api.Innersloth;
using Impostor.Api.Net;
using Impostor.Api.Net.Inner.Objects;
using Impostor.Api.Net.Inner.Objects.ShipSystems;
using Impostor.Api.Net.Messages;
using Impostor.Server.Net.Inner.Objects.Systems;
using Impostor.Server.Net.Inner.Objects.Systems.ShipStatus;
using Impostor.Server.Net.State;
using Microsoft.Extensions.Logging;

namespace Impostor.Server.Net.Inner.Objects
{
    internal partial class InnerShipStatus : InnerNetObject
    {
        private readonly ILogger<InnerShipStatus> _logger;
        private readonly IEventManager _eventManager;
        private readonly Game _game;
        private readonly Dictionary<SystemTypes, ISystemType> _systems;

        public InnerShipStatus(ILogger<InnerShipStatus> logger, IServiceProvider serviceProvider, IEventManager eventManager, Game game)
        {
            _logger = logger;
            _eventManager = eventManager;
            _game = game;

            _systems = new Dictionary<SystemTypes, ISystemType>
            {
                [SystemTypes.Electrical] = new SwitchSystem(_eventManager, _game),
                [SystemTypes.MedBay] = new MedScanSystem(_eventManager, _game),
                [SystemTypes.Reactor] = new ReactorSystem(_eventManager, _game),
                [SystemTypes.LifeSupp] = new LifeSuppSystemType(_eventManager, _game),
                [SystemTypes.Security] = new SecurityCameraSystemType(_eventManager, _game),
                [SystemTypes.Comms] = new HudOverrideSystem(_eventManager, _game),
                [SystemTypes.Doors] = new DoorsSystemType(_eventManager, _game),
            };

            _systems.Add(SystemTypes.Sabotage, new SabotageSystemType(new[]
            {
                (IActivatable)_systems[SystemTypes.Comms],
                (IActivatable)_systems[SystemTypes.Reactor],
                (IActivatable)_systems[SystemTypes.LifeSupp],
                (IActivatable)_systems[SystemTypes.Electrical],
            }));

            Components.Add(this);
        }

        public override ValueTask HandleRpc(ClientPlayer sender, ClientPlayer? target, RpcCalls call,
            IMessageReader reader)
        {
            switch (call)
            {
                case RpcCalls.CloseDoorsOfType:
                {
                    if (target == null || !target.IsHost)
                    {
                        throw new ImpostorCheatException($"Client sent {nameof(RpcCalls.CloseDoorsOfType)} to wrong destinition, must be host");
                    }

                    if (!sender.Character.PlayerInfo.IsImpostor)
                    {
                        throw new ImpostorCheatException($"Client sent {nameof(RpcCalls.CloseDoorsOfType)} as crewmate");
                    }

                    var systemType = (SystemTypes)reader.ReadByte();

                    break;
                }

                case RpcCalls.RepairSystem:
                {
                    if (target == null || !target.IsHost)
                    {
                        throw new ImpostorCheatException($"Client sent {nameof(RpcCalls.RepairSystem)} to wrong destinition, must be host");
                    }

                    var systemType = (SystemTypes)reader.ReadByte();
                    if (systemType == SystemTypes.Sabotage && !sender.Character.PlayerInfo.IsImpostor)
                    {
                        throw new ImpostorCheatException($"Client sent {nameof(RpcCalls.RepairSystem)} for {systemType} as crewmate");
                    }

                    var player = reader.ReadNetObject<InnerPlayerControl>(_game);
                    var amount = reader.ReadByte();

                    // TODO: Modify data (?)
                    break;
                }

                default:
                {
                    _logger.LogWarning("{0}: Unknown rpc call {1}", nameof(InnerShipStatus), call);
                    break;
                }
            }

            return default;
        }

        public override bool Serialize(IMessageWriter writer, bool initialState)
        {
            throw new NotImplementedException();
        }

        public override async ValueTask Deserialize(IClientPlayer sender, IClientPlayer? target, IMessageReader reader, bool initialState)
        {
            if (!sender.IsHost)
            {
                throw new ImpostorCheatException($"Client attempted to send data for {nameof(InnerShipStatus)} as non-host");
            }

            if (target != null)
            {
                throw new ImpostorCheatException($"Client attempted to send {nameof(InnerShipStatus)} data to a specific player, must be broadcast");
            }

            if (initialState)
            {
                // TODO: (_systems[SystemTypes.Doors] as DoorsSystemType).SetDoors();
                foreach (var systemType in SystemTypeHelpers.AllTypes)
                {
                    if (_systems.TryGetValue(systemType, out var system))
                    {
                        await system.Deserialize(reader, true);
                    }
                }
            }
            else
            {
                var count = reader.ReadPackedUInt32();

                foreach (var systemType in SystemTypeHelpers.AllTypes)
                {
                    // TODO: Not sure what is going on here, check.
                    if ((count & 1 << (int)(systemType & (SystemTypes.ShipTasks | SystemTypes.Doors))) != 0L)
                    {
                        if (_systems.TryGetValue(systemType, out var system))
                        {
                            await system.Deserialize(reader, false);
                        }
                    }
                }
            }
        }
    }
}
