using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    public class EEUnityEventCallerMenuAnimationFinished : EEUnityEventBinary
    {
        [SerializeField] private EEMenuView menuView = null;

        protected override void EEEnable()
        {
            base.EEEnable();
            menuView.FinishShowSuperAction.Event += EventOn;
            menuView.FinishHideSuperAction.Event += EventOff;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            menuView.FinishShowSuperAction.Event -= EventOn;
            menuView.FinishHideSuperAction.Event -= EventOff;
        }
    }
}
