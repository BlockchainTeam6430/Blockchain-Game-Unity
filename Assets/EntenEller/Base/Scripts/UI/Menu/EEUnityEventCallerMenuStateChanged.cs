using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    public class EEUnityEventCallerMenuStateChanged : EEBehaviour
    {
        [SerializeField] protected EESuperAction MenuStartShowSuperAction = null;
        [SerializeField] protected EESuperAction MenuStartHideSuperAction = null;
        [SerializeField] protected EESuperAction MenuFinishShowSuperAction = null;
        [SerializeField] protected EESuperAction MenuFinishHideSuperAction = null;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            if (GetParent<EEMenu>().IsActive)
            {
                StartShow();
                FinishShow();
            }
            GetParent<EEMenuView>().StartShowSuperAction.Event += StartShow;
            GetParent<EEMenuView>().StartHideSuperAction.Event += StartHide;
            GetParent<EEMenuView>().FinishShowSuperAction.Event += FinishShow;
            GetParent<EEMenuView>().FinishHideSuperAction.Event += FinishHide;
            if (GetParent<EEMenu>().IsActive)
            {
                MenuStartShowSuperAction.Call(ComponentID);
                MenuFinishShowSuperAction.Call(ComponentID);
            }
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetParent<EEMenuView>().StartShowSuperAction.Event -= StartShow;
            GetParent<EEMenuView>().StartHideSuperAction.Event -= StartHide;
            GetParent<EEMenuView>().FinishShowSuperAction.Event -= FinishShow;
            GetParent<EEMenuView>().FinishHideSuperAction.Event -= FinishHide;
        }
        
        private void StartShow()
        {
            MenuStartShowSuperAction.Call(ComponentID);
        }
        
        private void StartHide()
        {
            MenuStartHideSuperAction.Call(ComponentID);
        }
        
        private void FinishShow()
        {
            MenuFinishShowSuperAction.Call(ComponentID);
        }

        private void FinishHide()
        {
            MenuFinishHideSuperAction.Call(ComponentID);
        }
    }
}
