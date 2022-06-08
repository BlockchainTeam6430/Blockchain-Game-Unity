using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public abstract class EEUnityEventBinary : EEBehaviour
    {
        public EESuperAction OnSuperAction = new EESuperAction();
        public EESuperAction OffSuperAction = new EESuperAction();

        protected void EventOn()
        {
            OnSuperAction.Call(ComponentID);
        }

        protected void EventOff()
        {
            OffSuperAction.Call(ComponentID);
        }
    }
}
