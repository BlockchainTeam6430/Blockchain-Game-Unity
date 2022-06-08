using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LiteNetLib;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    [ExecutionOrder(-9000)]
    public class LiteNetClient : EEBehaviour, INetEventListener
    {
        public EESuperAction ConnectedSuperAction;
        public EESuperAction DisconnectedSuperAction;
        [SerializeField] private bool isConnectToLocalServer;
        [ShowIf("@!isConnectToLocalServer")] public string Address;
        public int Port = 6677;

        protected override void EEEnable()
        {
            base.EEEnable();
            EEClientGlobalEvents.SendMessageEvent += Send;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EEClientGlobalEvents.SendMessageEvent -= Send;
        }

        private static void Send(EEPacket packet, EEDeliveryType deliveryType)
        {
            if (EEServerGlobalEvents.ServerPeer == null) return;
            (EEServerGlobalEvents.ServerPeer as LiteNetPeer).NetPeer.Send(EEByteSerializer.Serialize(packet), LiteNetDeliveryConverter.DeliveryTypeConverter.Convert(deliveryType));    
        }

        public void Connect()
        {
            EEClientGlobalEvents.Start();
            var client = new NetManager(this);
            client.Start();
            if (isConnectToLocalServer) Address = "127.0.0.1";
            client.Connect(Address, Port, string.Empty);
            Timing.RunCoroutineSingleton(Receive(), ComponentID.ToString(), SingletonBehavior.Overwrite);
            
            IEnumerator<float> Receive()
            {
                while (true)
                {
                    yield return Timing.WaitForOneFrame;
                    client.PollEvents();
                }
            }
        }

        public void OnPeerConnected(NetPeer peer)
        {
            (EEServerGlobalEvents.ServerPeer as LiteNetPeer).Set(peer);
            EEClientGlobalEvents.Connect();
            ConnectedSuperAction.Call(ComponentID);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            DisconnectedSuperAction.Call(ComponentID);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var bytes = new byte[reader.AvailableBytes];
            EEDebug.Log(EEDebugTag.Client, "GOT data = " + bytes.Length);
            reader.GetBytes(bytes, reader.AvailableBytes);
            var packet = EEByteSerializer.Deserialize<EEPacket>(bytes);
            EEClientGlobalEvents.ReceivedMessage(packet);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            EEDebug.Log(EEDebugTag.Client, "PING = " + latency);
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            
        }
    }
}