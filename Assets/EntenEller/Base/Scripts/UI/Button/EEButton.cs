using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.UI.Button
{
    public class EEButton : EEUnityEvent
    {
        public Action<EEUnityEvent> ClickEvent;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<UnityEngine.UI.Button>().onClick.AddListener(Click);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<UnityEngine.UI.Button>().onClick.RemoveListener(Click);
        }

        private void Click()
        {
            Call();
            ClickEvent.Call(this);
        }
    }
}
