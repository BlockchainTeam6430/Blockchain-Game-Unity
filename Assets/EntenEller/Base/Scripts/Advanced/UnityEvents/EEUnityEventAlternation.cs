using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEventAlternation : EEBehaviour
    {
        [SerializeField] private List<EESuperAction> superActions;
        [SerializeField] private int index;
        
        public void Call()
        {
            superActions[index].Call(ComponentID);
            index++;
            if (index >= superActions.Count) index = 0;
        }

        public void SetIndex(int i)
        {
            index = i;
        }
    }
}
