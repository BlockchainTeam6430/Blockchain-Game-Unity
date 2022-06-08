using System;
using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    public static class EEPacketToServer
    {
        public static void Send(Type type, object data = null, EENetPoolObject netPoolObject = null,  EEDeliveryType delivery = EEDeliveryType.ReliableOrdered)
        {
            EENetSender.Send(EEServerGlobalEvents.ServerPeer, type, data, netPoolObject, delivery, true);
        }
    }
}