using System;
using System.Globalization;
using BestHTTP.WebSocket;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    [ExecutionOrder(-9999)]
    public class WebsocketClient : EEBehaviour
    {
        [SerializeField] private string url;
        private WebSocket webSocket;

        protected override void EEAwake()
        {
            base.EEAwake();
            EEClientGlobalEvents.SendMessageEvent += Send;
        }

        public void SetURL(string u)
        {
            url = u;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EEClientGlobalEvents.SendMessageEvent -= Send;
        }
        
        private void Send(EEPacket packet, EEDeliveryType deliveryType)
        {
            webSocket?.Send(EEByteSerializer.Serialize(packet));
        }
        
        public void On()
        {
            webSocket = new WebSocket(new Uri(url));
            
            webSocket.Open();
            
            webSocket.OnOpen += OnOpen;
            webSocket.OnBinary += OnBinary;
            webSocket.OnClosed += OnClosed;
            webSocket.OnError += OnError;
        }
        
        [Button]
        public void Off()
        {
            webSocket.OnOpen -= OnOpen;
            webSocket.OnBinary -= OnBinary;
            webSocket.OnClosed -= OnClosed;
            webSocket.OnError -= OnError;
            webSocket.Close();
        }

        private static void OnOpen(WebSocket websocket)
        {
            EEClientGlobalEvents.Connect();
        }
        
        private static void OnBinary(WebSocket websocket, byte[] data)
        {
            try
            {
                var packet = EEByteSerializer.Deserialize<EEPacket>(data);
                EEClientGlobalEvents.ReceivedMessage(packet);
            }
            catch {}
        }
        
        private static void OnClosed(WebSocket websocket, ushort code, string message)
        {
            EEClientGlobalEvents.Close();
        }
        
        private static void OnError(WebSocket websocket, string reason)
        {
            EEClientGlobalEvents.Close();
        }
    }
}
