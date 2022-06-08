using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Tests
{
    public class ClientSenderNaturalNumbers : EEBehaviour
    {
        private int number;

        public void Send()
        {
            number++;
            GetSelf<ClientSimpleSender>().SendInt(number);
        }
    }
}
