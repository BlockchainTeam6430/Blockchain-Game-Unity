using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EEUnityEventForClientOnly : EEUnityEvent
    {
        public override void Call()
        {
            if (EEClientGlobalEvents.IsConnected) base.Call();
        }
    }
}
