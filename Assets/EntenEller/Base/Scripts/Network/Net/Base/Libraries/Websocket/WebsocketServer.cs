using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Network;
using UnityEngine;
#if !UNITY_WEBGL
using WebSocketSharp;
using WebSocketSharp.Server;
#endif


namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    [ExecutionOrder(-9999)]
    public class WebsocketServer : EEBehaviourLoop
    {
    #if !UNITY_WEBGL
        [SerializeField] private bool isFloatyPort;
        public int Port = 4649;
        private WebSocketServer server;
        private HashSet<string> activeUsers = new HashSet<string>();
        private static List<NetData> cache = new List<NetData>();
        
        protected override void EEAwake()
        {
            base.EEAwake();
            
            EEServerGlobalEvents.SendMessageEvent += Send;

            void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType)
            {
                var services = server.WebSocketServices;
                var sessions = services.Hosts.First().Sessions;
                sessions.SendTo(EEByteSerializer.Serialize(packet), peer.ID);
            }
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Off();
        }

        public void On()
        {
            if (isFloatyPort)
            {
                var properties = IPGlobalProperties.GetIPGlobalProperties();
                var connections = properties.GetActiveTcpConnections();
                for (var i = Port; i < 7777; i++)
                {
                    print("CHECKING PORT = " + i);
                    if (connections.Any(a => a.LocalEndPoint.Port == i)) continue;
                    print("STARTING PORT = " + Port);
                    Port = i;
                    break;
                }
            }
            
            server = new WebSocketServer(System.Net.IPAddress.Any, Port, true);
            
            server.AddWebSocketService<ServerController>("/");
            server.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            server.SslConfiguration.ServerCertificate = new X509Certificate2(Application.streamingAssetsPath + "/output.pfx", "qwerty");
            server.Start();

            EEServerGlobalEvents.Start();
            EEServerGlobalEvents.Connect();
            
            Timing.RunCoroutine(Loop());

            IEnumerator<float> Loop()
            {
                while (true)
                {
                    yield return Timing.WaitForOneFrame;
                    
                    var services = server.WebSocketServices;
                    var sessions = services.Hosts.First().Sessions;
                    var ids = sessions.ActiveIDs.ToArray();

                    foreach (var id in ids)
                    {
                        if (activeUsers.Contains(id)) continue;
                        print("ENTER " + id);
                        activeUsers.Add(id);
                        var peer = WebsocketPeer.Convert(id);
                        EEServerGlobalEvents.ConnectClient(peer);
                    }
                    
                    foreach (var id in activeUsers.ToArray())
                    {
                        if (ids.Contains(id)) continue;
                        print("REMOVE " + id);
                        activeUsers.Remove(id);
                        var peer = WebsocketPeer.Convert(id);
                        WebsocketPeer.Kick(id);
                        EEServerGlobalEvents.DisconnectClient(peer);
                    }
                }
            }
        }

        public void Off()
        {
            server.Stop();
        }

        public void Kick(EEPeer peer)
        {
            activeUsers.Remove(peer.ID);
            WebsocketPeer.Kick(peer.ID);
            EEServerGlobalEvents.DisconnectClient(peer);
        }

        protected override void Loop()
        {
            base.Loop();
            lock (cache)
            {
                cache.ForEach(a => EEServerGlobalEvents.ReceiveMessage(a.Peer, a.Packet));
                cache.Clear();
            }
        }

        public class ServerController : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs msg)
            {
                base.OnMessage(msg);
                try
                {
                    var packet = EEByteSerializer.Deserialize<EEPacket>(msg.RawData);
                    var netData = new NetData
                    {
                        Peer = WebsocketPeer.Convert(ID),
                        Packet = packet
                    };
                    lock (cache)
                    {
                        cache.Add(netData);
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }
#endif
        public class NetData
        {
            public EEPeer Peer;
            public EEPacket Packet;
        }
    }
}
