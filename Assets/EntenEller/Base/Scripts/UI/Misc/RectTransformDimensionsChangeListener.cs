using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine.EventSystems;

namespace Plugins.EntenEller.Base.Scripts.UI.Misc
{
    public class RectTransformDimensionsChangeListener : UIBehaviour
    {
        public event Action ChangedEvent;
        
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            ChangedEvent.Call();
        }
    }
}
