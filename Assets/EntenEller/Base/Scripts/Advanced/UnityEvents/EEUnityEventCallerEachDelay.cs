using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Plugins.EntenEller.Base.Scripts.Timers;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents
{
    public class EEUnityEventCallerEachDelay : EEUnityEvent
    {
        [SerializeField] protected FloatRandomOrDefined delayToRepeat;

        public override void Call()
        {
            if (delayToRepeat.Value == 0)
            {
                EEException.Call(this, "Delay is almost zero! " + gameObject.name);
                return;
            }

            SetTimer();
        }

        private void CallRepeat()
        {
            Task();
            CallWithTimer();
        }

        private void SetTimer()
        {
            EETime.StopAllTimersForComponentID(ComponentID);
            CallWithTimer();
        }

        private void CallWithTimer()
        {
            if (this == null) return;
            if (!gameObject.activeInHierarchy) return;
            EETime.StartTimer(new EETime.EETimerData
            {
                ComponentID = ComponentID,
                Action = CallRepeat,
                FinalTime = delayToRepeat.Value
            });
        }
    }
}
