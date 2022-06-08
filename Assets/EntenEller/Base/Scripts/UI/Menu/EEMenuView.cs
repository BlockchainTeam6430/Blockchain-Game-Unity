using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Animators;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Menu
{
    public class EEMenuView : EEBehaviour
    {
        private enum State
        {
            Show,
            Hide,
            Unknown
        }

        [SerializeField] [ReadOnly]  private State state = State.Unknown;
        
        public EESuperAction StartShowSuperAction;
        public EESuperAction StartHideSuperAction;
        public EESuperAction FinishShowSuperAction;
        public EESuperAction FinishHideSuperAction;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEAnimatorUI>().FinishedSuperAction.Event += Finished;
            GetSelf<EEMenu>().ShowEvent += StartShow;
            GetSelf<EEMenu>().HideEvent += StartHide;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEAnimatorUI>().FinishedSuperAction.Event -= Finished;
            GetSelf<EEMenu>().ShowEvent -= StartShow;
            GetSelf<EEMenu>().HideEvent -= StartHide;
        }

        private void Finished()
        {
            switch (state)
            {
                case State.Show:
                    FinishShowSuperAction.Call(ComponentID);
                    break;
                case State.Hide:
                    FinishHideSuperAction.Call(ComponentID);
                    break;
                case State.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void StartShow()
        {
            StartShowSuperAction.Call(ComponentID);
            state = State.Show;
            PlayEffect(true);
        }
        
        private void StartHide()
        {
            StartHideSuperAction.Call(ComponentID);
            state = State.Hide;
            PlayEffect(false);
        }
        
        private void PlayEffect(bool isVisible)
        {
            if (isVisible)
            {
                GetSelf<EEAnimatorUI>().PlayForward("Main");
            }
            else GetSelf<EEAnimatorUI>().PlayBackward("Main");
        }
    }
}
