using System.Collections.Generic;
using System.Linq;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using UnityEngine;

namespace Project.Scripts.Network.Lobby
{
    public class LobbyServerSubservers : EEBehaviourLoop, IEENetReceiverServer
    {
        public Dictionary<EEPeer, SubserverData> Servers = new Dictionary<EEPeer, SubserverData>();

        protected override void EEAwake()
        {
            base.EEAwake();
            EEServerGlobalEvents.ClientDisconnectedEvent += Disconnected;
            Timing.Instance.TimeBetweenSlowUpdateCalls = 5f;
        }

        protected override void Loop()
        {
            base.Loop();
            print("SERVER = " + Servers.Count + "; CLIENT = " + LobbyServerClients.clients.Count());
            foreach (var server in Servers)
            {
                EEPacketToClient.Send(server.Key, typeof(LobbySubserver), Time.time);
            }
        }

        private void Disconnected(EEPeer peer)
        {
            if (Servers.ContainsKey(peer)) Remove(peer);
        }

        public void Remove(EEPeer server)
        {
            Servers.Remove(server);
        }
        
        public void ReceiveAsServer(EEPeer peer, object data)
        {
            if (!(data is SubserverData)) return;
            if (Servers.ContainsKey(peer)) return;
            var subserverData = (SubserverData) data;
            subserverData.PasswordVerification = string.Empty;
            Servers.Add(peer, subserverData);
        }
    }
}
