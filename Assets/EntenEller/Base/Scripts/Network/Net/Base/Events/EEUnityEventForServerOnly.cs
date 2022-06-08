using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events
{
    public class EEUnityEventForServerOnly : EEUnityEvent
    {
        public override void Call()
        {
            if (EEServerGlobalEvents.IsActive) base.Call();
        }
    }
}
