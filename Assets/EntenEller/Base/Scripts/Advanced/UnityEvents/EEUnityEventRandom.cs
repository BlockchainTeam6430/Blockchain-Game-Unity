using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEventRandom : EEBehaviour
    {
        [SerializeField] private List<EESuperAction> superActions;

        public void Call()
        {
            superActions.GetRandom().Call(ComponentID);
        }
    }
}
