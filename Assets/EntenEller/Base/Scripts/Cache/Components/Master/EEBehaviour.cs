using Plugins.EntenEller.Base.Scripts.Cache.Components.Parent;
using Plugins.EntenEller.Base.Scripts.Timers;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Master
{
    public abstract class EEBehaviour : CachedBehaviourParent
    {
        public void Cancel()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
        }
    }
}
