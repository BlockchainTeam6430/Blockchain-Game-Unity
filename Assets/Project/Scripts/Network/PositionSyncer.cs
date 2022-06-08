using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class PositionSyncer : EEBehaviourLoop, IEENetReceiverClient, IEENetReceiverServer
    {
        private Vector3 position;

        protected override void EEAwake()
        {
            base.EEAwake();
            position = GetSelf<Transform>().position;
        }

        protected override void Loop()
        {
            base.Loop();
            if (!GetSelf<PlayerOwner>().IsMine && !EEServerGlobalEvents.IsActive) GetSelf<CharacterMover>().Move(position - GetSelf<Transform>().position);
            else
            {
                EEPacketToServer.Send(typeof(PositionSyncer), GetSelf<Transform>().position, GetSelf<EENetPoolObject>());
            }
        }

        public void ReceiveAsClient(object data)
        {
            position = (Vector3) data;
        }

        public void ReceiveAsServer(EEPeer peer, object data)
        {
            if (!(data is Vector3)) return;
            if (GetSelf<PlayerOwner>().Peer != peer) return;
            position = (Vector3) data;
            GetSelf<Transform>().position = position;
            EEServerGlobalEvents.Clients.ForEach(a =>
            {
                if (a != peer) EEPacketToClient.Send(a, typeof(PositionSyncer), position, GetSelf<EENetPoolObject>());
            });
        }
    }
}
