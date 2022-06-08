using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Animators;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    [RequireComponent(typeof(EEMenuView))]
    [RequireComponent(typeof(EEAnimatorUI))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]

    public class EEMenu : EEBehaviour
    {
        internal Action ShowEvent;
        internal Action HideEvent;
        public bool IsActive;
        private bool previousState;
        public bool IgnoreMenuSystem;
        
        protected override void EEAwake()
        {
            previousState = IsActive;
        }
        
        public void SetState(bool isOn)
        {
            if (previousState == isOn) return;
            previousState = IsActive = isOn;
            (IsActive ? ShowEvent : HideEvent).Call();
        }
    }
}