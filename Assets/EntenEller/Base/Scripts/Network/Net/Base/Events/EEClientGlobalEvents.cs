using System;
using EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Network;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public static class EEClientGlobalEvents
    {
        public static event Action StartedEvent;
        public static event Action ConnectedEvent;
        public static event Action DisconnectedEvent;
        public static event Action ClosedEvent;
        
        public static event Action<EEPacket> ReceivedMessageEvent;
        public static event Action<EEPacket, EEDeliveryType> SendMessageEvent;
        
        public static bool IsConnected;
        
        public static void Start()
        {
            EEDebug.Log(EEDebugTag.Client, "[START]");
            StartedEvent.Call();
        }

        public static void Connect()
        {
            EEDebug.Log(EEDebugTag.Client, "[CONNECTED]");
            IsConnected = true;
            ConnectedEvent.Call();
        }

        public static void Disconnect()
        {
            EEDebug.Log(EEDebugTag.Client, "[DISCONNECTED]");
            IsConnected = false;
            DisconnectedEvent.Call();
        }
        
        public static void ReceivedMessage(EEPacket packet)
        {
            EENetReceiver.Receive(packet);
            ReceivedMessageEvent.Call(packet);
        }
        
        public static void Send(EEPacket packet, EEDeliveryType deliveryType)
        {
            SendMessageEvent.Call(packet, deliveryType);
        }
        
        public static void Close()
        {
            IsConnected = false;
            ClosedEvent.Call();
        }
    }
}
