using System;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender
{
    public static class EENetSender
    {
        private static readonly EEPacket packet = new EEPacket();
        
        public static void Send(EEPeer peer, Type type, object data = null, EENetPoolObject poolObject = null, EEDeliveryType delivery = EEDeliveryType.ReliableOrdered, bool isForServer = false)
        {
            packet.ScriptName = type.FullName;
            packet.IsForServer = isForServer;
            packet.Data = data;
            
            if (poolObject)
            {
                var (i, j) = EESingleton.Get<EENetPoolManager>().GetIJ(poolObject);
                packet.I = i;
                packet.J = j;
            }
            else
            {
                packet.I = -1;
            }

            if (isForServer)
            {
                EEClientGlobalEvents.Send(packet, delivery);
            }
            else
            {
                EEServerGlobalEvents.Send(peer, packet, delivery);
            }
        }
    }
}
