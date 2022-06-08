using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEBoundToggle : EEBehaviour
    {
        public string specialTag;
        private static int frame;
        
        //TODO Add allow switch off
        
        private void ValueChanged(bool isOn)
        {
            var toggleGroup = GetChild<UnityEngine.UI.Toggle>().group;
            if (toggleGroup != null)
            {
                if (!isOn) return;
            }
            if (frame == Time.frameCount) return;
            frame = Time.frameCount;
            var boundToggles = EEComponentUtils.FindAll<EEBoundToggle>().Where(a => a.specialTag == specialTag).ToList();
            foreach (var boundToggle in boundToggles)
            {
                boundToggle.GetSelf<EEToggle>().SetState(isOn);
            }
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEToggle>().ValueChangedEvent += ValueChanged;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEToggle>().ValueChangedEvent -= ValueChanged;
        }
    }
}
