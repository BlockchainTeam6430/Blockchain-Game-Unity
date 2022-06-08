using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    [ExecutionOrder(-9000)]
    public class LiteNetServer : EEBehaviourLoop, INetEventListener
    {
        public int Port = 6677;
        private NetManager server;
        public EESuperAction StartedSuperAction;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            server = new NetManager(this);
            server.Start(Port);
            EEServerGlobalEvents.SendMessageEvent += Send;
            StartedSuperAction.Call(ComponentID);
            
            Timing.RunCoroutine(Receive());
            IEnumerator<float> Receive()
            {
                while (true)
                {  
                    yield return Timing.WaitForOneFrame;
                    server.PollEvents();
                }
            }
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            server.Stop();
            EEServerGlobalEvents.SendMessageEvent -= Send;
        }

        private static void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType)
        {
            EEDebug.Log(EEDebugTag.ServerSend, "SENDING = " + packet.Data);
            (peer as LiteNetPeer).NetPeer.Send(EEByteSerializer.Serialize(packet), LiteNetDeliveryConverter.DeliveryTypeConverter.Convert(deliveryType));    
        }
        
        public void OnPeerConnected(NetPeer peer)
        {
            EEDebug.Log(EEDebugTag.Server, "Connected = " + peer.Id);
            EEServerGlobalEvents.ConnectClient(LiteNetPeer.Convert(peer));
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            EEDebug.Log(EEDebugTag.Server, "Disconnected = " + peer.Id);
            EEServerGlobalEvents.DisconnectClient(LiteNetPeer.Convert(peer));
            LiteNetPeer.Kick(peer);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            EEDebug.Log(EEDebugTag.Server, "Network error = " + socketError);
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var bytes = new byte[reader.AvailableBytes];
            reader.GetBytes(bytes, reader.AvailableBytes);
            var packet = EEByteSerializer.Deserialize<EEPacket>(bytes);
            var netPeer = LiteNetPeer.Convert(peer);
            EEServerGlobalEvents.ReceiveMessage(netPeer, packet);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            EEDebug.Log(EEDebugTag.Server, "ReceiveUnconnected");
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            request.Accept();
        }
    }
}
