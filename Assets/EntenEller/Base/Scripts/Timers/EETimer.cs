using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Timers
{
    public class EETimer : EEBehaviourLoop
    {
        public EESuperAction TimerBeginSuperAction;
        public EESuperAction TimerPauseSuperAction;
        public EESuperAction TimerResumeSuperAction;
        public EESuperAction TimerEndSuperAction;
        public EESuperAction TimerBreakSuperAction;
        public EESuperAction TimerAlwaysSuperAction;
        public event Action<float> TimerChangeEvent;
        
        public float StartingTime;
        public float FinalTime;
        public float CurrentTime;
        
        [SerializeField] private bool IsUnscaled;
        private EETime.EETimerData timerData = new EETime.EETimerData();

        protected override void EEAwake()
        {
            base.EEAwake();
            timerData.ComponentID = ComponentID;
            timerData.Action = Complete;
        }
        
        protected override void EEEnable()
        {
            base.EEEnable();
            timerData.OnUpdateEvent += TimerUpdate;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            timerData.OnUpdateEvent -= TimerUpdate;
        }

        public void Begin()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            timerData.CurrentTime = StartingTime;
            timerData.FinalTime = FinalTime;
            TimerBeginSuperAction.Call(ComponentID);
            EETime.StartTimer(timerData);
        }

        public void Pause()
        {
            EETime.PauseTimer(Complete);
            TimerPauseSuperAction.Call(ComponentID);
        }
        
        public void Resume()
        {
            EETime.ResumeTimer(Complete);
            TimerResumeSuperAction.Call(ComponentID);
        }

        public void Complete()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            TimerEndSuperAction.Call(ComponentID);
            TimerAlwaysSuperAction.Call(ComponentID);
        }
        
        public void Break()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            TimerBreakSuperAction.Call(ComponentID);
            TimerAlwaysSuperAction.Call(ComponentID);
        }

        private void TimerUpdate()
        {
            CurrentTime = timerData.CurrentTime;
            TimerChangeEvent.Call(CurrentTime);
        }
    }
}
