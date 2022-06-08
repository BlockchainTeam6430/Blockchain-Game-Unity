using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Animators;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEToggle : EEBehaviour
    {
        public event Action<bool> ValueRawChangedEvent;
        public event Action<bool> ValueChangedEvent;
        public EESuperAction ValueIsOnSuperAction;
        public EESuperAction ValueIsOffSuperAction;
        
        private bool previousIsOn;
        [ReadOnly] public bool IsOn;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            IsOn = previousIsOn = GetSelf<Toggle>().isOn;
            GetSelf<Toggle>().onValueChanged.AddListener(ValueChangedToggle);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<Toggle>().onValueChanged.RemoveListener(ValueChangedToggle);
        }

        public void SetOn()
        {
            SetState(true);
        }
        
        public void SetOff()
        {
            SetState(false);
        }
        
        public void SetState(bool isOn)
        {
            IsOn = isOn;
            GetSelf<Toggle>().isOn = IsOn;
        }
        
        private void ValueChangedToggle(bool isOn)
        {
            IsOn = isOn;
            ValueChanged(isOn);
        }

        private void ValueChanged(bool isOn, bool isAnimated = true)
        {
            ValueRawChangedEvent.Call(isOn);
            if (previousIsOn == isOn) return;
            previousIsOn = isOn;

            if (isOn)
            {
                ValueIsOnSuperAction.Call(ComponentID);
                if (isAnimated) GetChild<EEAnimatorUI>().PlayForward("Main");
            }
            else
            {
                ValueIsOffSuperAction.Call(ComponentID);
                if (isAnimated) GetChild<EEAnimatorUI>().PlayBackward("Main");
            }
                
            ValueChangedEvent.Call(isOn);
        }
    }
}