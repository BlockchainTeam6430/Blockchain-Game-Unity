using System.Net;
using System.Net.Sockets;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Network.UDP
{
    public class EEUDP : EEBehaviourLoop
    {
        public int PortSend, PortReceive;
        public string URL;
        
        public UdpClient Client;
        public IPEndPoint EndPoint;

        public EESuperAction OnSuperAction, OffSuperAction;
        
        public void On()
        {
            Client = new UdpClient(PortReceive);
            var address = URL != string.Empty ? IPAddress.Parse(URL) : IPAddress.Any;
            EndPoint = new IPEndPoint(address, PortSend);
            OnSuperAction.Call(ComponentID);
        }

        public void Off()
        {
            Client?.Close();
            Client?.Dispose();
            OffSuperAction.Call(ComponentID);
        }
    }
}