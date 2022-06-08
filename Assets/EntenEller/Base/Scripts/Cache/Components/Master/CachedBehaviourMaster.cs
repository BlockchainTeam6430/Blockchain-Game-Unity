using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Master
{
    public abstract class CachedBehaviourMaster : EEBehaviourInit
    {
        protected T CreateCachedComponent<T>() where T : CachedComponent
        {
            if (gameObject.GetComponent<T>().IsNull()) gameObject.AddComponent(typeof(T));
            return gameObject.GetComponent<T>();
        }
    }
}