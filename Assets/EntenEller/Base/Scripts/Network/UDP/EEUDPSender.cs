using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Serialization;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.UDP
{
    public class EEUDPSender : EEBehaviour
    {
        public void Send(byte[] data)
        {
            GetSelf<EEUDP>().Client.Send(data, data.Length, GetSelf<EEUDP>().URL, GetSelf<EEUDP>().PortSend);
        }

        public void Send(string s)
        {
            Send(SerializationUtility.SerializeValue(s, DataFormat.Binary));
        }
        
        public void SendRandomNumber()
        {
            Send(SerializationUtility.SerializeValue(Random.Range(0f, 1f), DataFormat.Binary));
        }
    }
}