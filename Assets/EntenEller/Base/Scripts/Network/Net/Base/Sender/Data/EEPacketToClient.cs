using System;
using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Affiliation;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    public static class EEPacketToClient
    {
        public static void Send(EEPlayer client, Type type, object data = null, EENetPoolObject netPoolObject = null, EEDeliveryType delivery = EEDeliveryType.ReliableOrdered)
        {
            var id = client.TagPlayer;
            EENetSender.Send(WebsocketPeer.Convert(id), type, data, netPoolObject, delivery);
        }
        
        public static void Send(EEPeer client, Type type, object data = null, EENetPoolObject netPoolObject = null, EEDeliveryType delivery = EEDeliveryType.ReliableOrdered)
        {
            EENetSender.Send(client, type, data, netPoolObject, delivery);
        }
    }
}