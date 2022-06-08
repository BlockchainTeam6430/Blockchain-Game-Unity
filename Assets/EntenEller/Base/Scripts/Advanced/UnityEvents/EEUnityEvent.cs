using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEvent : EEBehaviour
    {
        [SerializeField] private bool callOnAwake = false;
        [SerializeField] private bool callOnEnable = false;
        
        [SerializeField] protected bool IsUnscaled;
        
        [SerializeField] protected FloatRandomOrDefined delayToStart;
        
        public EESuperAction SuperAction = new EESuperAction();

        protected override void EEAwake()
        {
            base.EEAwake();
            if (callOnAwake) Call();
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            if (callOnEnable) Call();
        }
        
        public virtual void Call()
        {
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = Task,
                FinalTime = delayToStart.Value
            });
        }
        
        protected void Task()
        {
            SuperAction.Call(ComponentID);
        }
    }
}