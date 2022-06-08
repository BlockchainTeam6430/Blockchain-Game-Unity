using System;
using System.Collections.Generic;
using EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public static class EEServerGlobalEvents
    {
        public static event Action StartedEvent;
        public static event Action ConnectedEvent;
        
        public static event Action<EEPeer> ClientConnectedEvent;
        public static event Action<EEPeer> ClientDisconnectedEvent;
        
        public static event Action<EEPeer, EEPacket, EEDeliveryType> SendMessageEvent;

        public static bool IsActive;
        public static readonly EEPeer ServerPeer = new EEPeer();
        public static List<EEPeer> Clients = new List<EEPeer>();
        
        public static void Start()
        {
            EEDebug.Log(EEDebugTag.Server, "[START]");
            StartedEvent.Call();
            IsActive = true;
        }

        public static void Connect()
        {
            EEDebug.Log(EEDebugTag.Server, "[CONNECTED]");
            ConnectedEvent.Call();
        }

        public static void ReceiveMessage(EEPeer peer, EEPacket packet)
        {
            EENetReceiver.Receive(packet, peer);
        }
        
        public static void Send(EEPeer peer, EEPacket packet, EEDeliveryType deliveryType)
        {
            SendMessageEvent.Call(peer, packet, deliveryType);
        }
        
        public static void ConnectClient(EEPeer peer)
        {
            EEDebug.Log(EEDebugTag.Server, "[CONNECTED CLIENT] " + peer.ID);
            Clients.Add(peer);
            ClientConnectedEvent.Call(peer);
        }
        
        public static void DisconnectClient(EEPeer peer)
        {
            EEDebug.Log(EEDebugTag.Server, "[DISCONNECTED CLIENT] " + peer.ID);
            Clients.Remove(peer);
            ClientDisconnectedEvent.Call(peer);
        }
    }
}
