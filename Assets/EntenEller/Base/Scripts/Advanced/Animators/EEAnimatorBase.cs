using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Plugins.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EEAnimatorBase : EEBehaviourLoop
    {
        public string CurrentAnimationName;
        public FloatApproach TimeApproach;
        public Approach ApproachStyleForward, ApproachStyleBackward;
        
        public EESuperAction StartedSuperAction;
        public EESuperAction FinishedSuperAction;
        public event Action<float> UpdateTimeEvent;
        
        [SerializeField] private bool isLoop;
        public float DelayForward, DelayBackward;
        public bool IsOver;

        protected override void EEEnable()
        {
            base.EEEnable();
            TimeApproach.ReachedSuperAction.Event += Reached;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            TimeApproach.ReachedSuperAction.Event -= Reached;
        }
        
        private void Reached()
        {
            FinishedSuperAction.Call(ComponentID);
        }
        
        public void SetTime(float time)
        {
            TimeApproach.Current = time;
        }
        
        public void PlayForward(string animationName)
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            TimeApproach.Approach = ApproachStyleForward;
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = () =>
                {
                    Play(animationName, 1);
                },
                FinalTime = DelayForward
            });
        }
        
        public void PlayBackward(string animationName)
        {
            EETime.StopAllTimersForComponentID(ComponentID); 
            TimeApproach.Approach = ApproachStyleBackward;
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = () =>
                {
                    Play(animationName, 0);
                },
                FinalTime = DelayBackward
            });
        }

        public void Play(string animationName, float targetTime)
        {
            CurrentAnimationName = animationName;
            TimeApproach.SetTarget(targetTime);
            StartedSuperAction.Call(ComponentID);
            IsOver = false;
        }
        
        protected void PlayAnimation()
        {
            TimeApproach.Proceed();
            IsOver = TimeApproach.IsReached;
            if (!IsOver)
            {
                UpdateTimeEvent.Call(TimeApproach.Current);
            }
            else
            {
                if (isLoop)
                {
                    TimeApproach.SetTarget(TimeApproach.Target == 1 ? 0 : 1);
                }
            }
            GetSelf<Animator>().Play(CurrentAnimationName, -1, TimeApproach.Current);
        }

        public void SetTrueBool(string nameBool)
        {
            GetSelf<Animator>().SetBool(nameBool, true);
        }
        
        public void SetFalseBool(string nameBool)
        {
            GetSelf<Animator>().SetBool(nameBool, false);
        }

        public void ChangeForwardDelay(float delay)
        {
            DelayForward = delay;
        }

        public void ChangeBackwardDelay(float delay)
        {
            DelayBackward = delay;
        }

    }
}
