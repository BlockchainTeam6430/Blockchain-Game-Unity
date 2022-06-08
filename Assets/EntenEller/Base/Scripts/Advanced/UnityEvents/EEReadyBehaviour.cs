using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public abstract class EEReadyBehaviour : EEBehaviour
    {
        public EESuperAction NotReadySuperAction;
        public EESuperAction ReadySuperAction;
        [ReadOnly] public bool IsReady = true;
        
        protected void NotReady()
        {
            IsReady = false;
            NotReadySuperAction.Call(ComponentID);
        }
        
        protected void Ready()
        {
            IsReady = true;
            ReadySuperAction.Call(ComponentID);
        }
    }
}