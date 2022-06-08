using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Tags
{
    public class UnityEventCallerByEETags : EEBehaviour
    {
        [SerializeField] private List<string> eeTags;
        [SerializeField] private bool isSingleRandom;
        
        public void FindAndCall()
        {
            foreach (var list in eeTags.Select(EETagUtils.FindEETagsInScenes))
            {
                if (isSingleRandom)
                {
                    Call(list.ToList().GetRandom());
                }
                else
                {
                    list.ForEach(Call);
                }
                void Call(EETagHolder obj)
                {
                    if (obj.GetSelf<EEUnityEvent>()) obj.GetSelf<EEUnityEvent>().SuperAction.Call(ComponentID);
                }
            }
        }
    }
}