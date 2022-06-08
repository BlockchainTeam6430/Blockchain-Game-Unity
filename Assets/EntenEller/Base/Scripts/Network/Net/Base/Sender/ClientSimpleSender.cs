using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender
{
    public class ClientSimpleSender : EEBehaviour
    {
        public void SendString(string data)
        {
            EEPacketToServer.Send(typeof(string), data);
        }
        
        public void SendInt(int data)
        {
            EEPacketToServer.Send(typeof(int), data);
        }
    }
}
