using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    [ExecutionOrder(2000)]
    public abstract class EEBehaviourEEMenu : EEUnityEventCallerMenuStateChanged
    {
        [SerializeField] private Segment segment = Segment.Update;

        protected override void EEEnable()
        {
            MenuStartShowSuperAction.Event += MenuStartShow;
            MenuStartHideSuperAction.Event += MenuStartHide;
            MenuFinishShowSuperAction.Event += MenuFinishShow;
            MenuFinishHideSuperAction.Event += MenuFinishHide;
            base.EEEnable();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            MenuStartShowSuperAction.Event -= MenuStartShow;
            MenuStartHideSuperAction.Event -= MenuStartHide;
            MenuFinishShowSuperAction.Event -= MenuFinishShow;
            MenuFinishHideSuperAction.Event -= MenuFinishHide;
        }

        protected virtual void MenuStartShow()
        {
            Timing.RunCoroutine(Loop(), segment, ComponentID);
        }
        
        private IEnumerator<float> Loop()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                if (enabled) MenuLoop();
            }
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            Timing.KillCoroutines(ComponentID);
        }

        protected virtual void MenuStartHide()
        {
            Timing.KillCoroutines(ComponentID);
        }
        
        protected virtual void MenuFinishShow() {}
        
        protected virtual void MenuFinishHide() {}
        
        protected virtual void MenuLoop() {}
    }
}
