using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Timers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    [Serializable]
    public class EESuperAction
    {
        [SerializeField] private List<EESuperActionAttribute> Actions = new List<EESuperActionAttribute>();
        public event Action Event;
    #if UNITY_EDITOR
        [ReadOnly] public int FrameOfLastCall;
    #endif
        
        public void Call(int componentID = int.MinValue)
        {
#if UNITY_EDITOR
            FrameOfLastCall = Time.frameCount;
#endif
            Event.Call();
            foreach (var action in Actions)
            {
                EETime.StartTimer(new EETime.EETimerData
                {
                    ComponentID = componentID,
                    Action = () =>
                    {
                        action.UnityEvent?.Invoke();
                    },
                    FinalTime = action.Delay.Value,
                    IsIgnoreSceneSwitching = action.IsIgnoreSceneSwitching
                });
            }
        }
        
        [Serializable]
        public struct EESuperActionAttribute
        {
            public FloatRandomOrDefined Delay;
            public bool IsIgnoreSceneSwitching;
            public UnityEvent UnityEvent;
        }
    }
}