using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Animators
{
    [RequireComponent(typeof(Animator))]
    public class EEAnimatorUI : EEAnimatorBase
    {
        private EEMenuView menu;

        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<Animator>().enabled = false;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            menu = GetSelf<EEMenuView>();
            if (menu.IsNull()) menu = GetParent<EEMenuView>();
            menu.StartShowSuperAction.Event += StartAnimation;
            menu.FinishHideSuperAction.Event += StopAnimation;
            if (menu.GetSelf<EEMenu>().IsActive) StartAnimation();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            menu.StartShowSuperAction.Event -= StartAnimation;
            menu.FinishHideSuperAction.Event -= StopAnimation;
        }

        private void StartAnimation()
        {
            GetSelf<Animator>().enabled = true;
            Timing.RunCoroutine(Animate(), ComponentID);
        }
        
        private void StopAnimation()
        {
            GetSelf<Animator>().enabled = false;
            Timing.KillCoroutines(ComponentID);
        }

        private IEnumerator<float> Animate()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                PlayAnimation();
            }
        }
    }
}