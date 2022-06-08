using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public class GameObjectStateByEETag : EEBehaviour
    {
        [SerializeField] private List<string> eeTags;
        
        public void On()
        {
            SetState(true);
        }

        public void Off()
        {
            SetState(false);
        }

        public void SetState(bool isOn)
        {
            foreach (var eeTag in eeTags)
            {
                EETagUtils.FindEETagsInScenes(eeTag).ForEach(a => a.GetEEGameObject().SetState(isOn));
            }
        }
    }
}
