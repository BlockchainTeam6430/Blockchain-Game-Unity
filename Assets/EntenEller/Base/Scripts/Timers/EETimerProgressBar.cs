using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Timers
{
    public class EETimerProgressBar : EEBehaviour
    {        
        [SerializeField] private EETimer timer;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            timer.TimerChangeEvent += TimerChange;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            timer.TimerChangeEvent -= TimerChange;
        }
        
        private void TimerChange(float time)
        {
            GetSelf<Slider>().value = time / timer.StartingTime;
        }
    }
}
