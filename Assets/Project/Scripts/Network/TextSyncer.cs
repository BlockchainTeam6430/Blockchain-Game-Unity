using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.UI.Text;

namespace Project.Scripts.Network
{
    public class TextSyncer : EEBehaviour, IEENetReceiverClient
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            Timing.RunCoroutine(Send());
        }

        private IEnumerator<float> Send()
        {
            yield return Timing.WaitForOneFrame;
            var data = GetChild<EEText>().Data;
            EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(TextSyncer), data, GetSelf<EENetPoolObject>()));
        }
        
        public void ReceiveAsClient(object data)
        {
            GetChild<EEText>().SetData((string) data);
        }
    }
}
