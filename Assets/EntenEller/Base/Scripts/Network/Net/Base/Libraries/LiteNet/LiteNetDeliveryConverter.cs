using System.Collections.Generic;
using LiteNetLib;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    public static class LiteNetDeliveryConverter
    {
        public static class DeliveryTypeConverter
        {
            private static readonly Dictionary<EEDeliveryType, DeliveryMethod> dictionary = new Dictionary<EEDeliveryType, DeliveryMethod>()
            {
                {EEDeliveryType.Unreliable, DeliveryMethod.Unreliable},
                {EEDeliveryType.ReliableOrdered,  DeliveryMethod.ReliableOrdered},
                {EEDeliveryType.ReliableUnordered,  DeliveryMethod.ReliableUnordered},
                {EEDeliveryType.Sequenced,  DeliveryMethod.Sequenced},
                {EEDeliveryType.ReliableSequenced,  DeliveryMethod.ReliableSequenced}
            };

            public static DeliveryMethod Convert(EEDeliveryType delivery)
            {
                return dictionary[delivery];
            }
        }
    }
}
