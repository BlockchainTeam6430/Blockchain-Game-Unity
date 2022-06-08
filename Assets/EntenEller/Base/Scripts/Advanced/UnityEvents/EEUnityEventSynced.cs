using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEventSynced : EEUnityEvent
    {
        [SerializeField] private EEGameObjectFinder objectFinder;
        [SerializeField] private bool isOnlyForActiveGameObjects;
        
        public void CallSynced()
        {
            var list = objectFinder.GetAll(this);
            if (isOnlyForActiveGameObjects) list = list.Where(a => a.IsActive).ToList();
            list.ForEach(a => a.GetSelf<EEUnityEventSynced>().Call());
        }
    }
}
