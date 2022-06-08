using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Project.Scripts.Network.Lobby
{
    public class LobbyServerClients : EEBehaviourLoop, IEENetReceiverServer
    {
        public static Dictionary<EEPeer, int> clients = new Dictionary<EEPeer, int>();

        protected override void EEAwake()
        {
            base.EEAwake();
            EEServerGlobalEvents.ClientDisconnectedEvent += Disconnected;
        }

        private void Disconnected(EEPeer peer)
        {
            if (clients.ContainsKey(peer)) clients.Remove(peer);
        }

        public void ReceiveAsServer(EEPeer peer, object data)
        {
            if (clients.ContainsKey(peer)) return;
            clients.Add(peer, 0);
            clients[peer] = (int) data;
            EEDebug.Log(peer.ID + ":" + data);
        }

        protected override void Loop()
        {
            base.Loop();
            
            var single = clients.Where(kv => kv.Value == 1).ToList();
            var pvp = clients.Where(kv => kv.Value == 2).ToList();

            foreach (var kv in single)
            {
                SendClientsToSubserver(new List<EEPeer> {kv.Key});
            }

            for (var i = 0; i < pvp.Count; i += 2)
            {
                var kv = pvp[i];
                if (i + 1 >= pvp.Count) break;
                var peer1 = kv.Key;
                var peer2 = pvp[i + 1].Key;
                SendClientsToSubserver(new List<EEPeer>() {peer1, peer2});
            }
        }

        public void SendClientsToSubserver(List<EEPeer> peers)
        {
            var server = GetSelf<LobbyServerSubservers>().Servers.FirstOrDefault(a => a.Value.MaxPlayers == peers.Count);
            if (server.Key == null) return;
            foreach (var peer in peers)
            {
                clients.Remove(peer);
            }
            GetSelf<LobbyServerSubservers>().Servers.Remove(server.Key);
            #if !UNITY_WEBGL
            EESingleton.Get<WebsocketServer>().Kick(server.Key);
            #endif
            for (var i = 0; i < peers.Count; i++)
            {
                var peer = peers[i];
                var list = new List<string>
                {
                    server.Value.URL, 
                    server.Value.PasswordsTemp[i]
                };
                EEPacketToClient.Send(peer, typeof(LobbyClient), list);
            }
        }
    }
}
