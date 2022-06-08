using System;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Timers
{
    public class EETimerEEText : EEBehaviour
    {
        [SerializeField] private EETimer timer;
        [SerializeField] private string format = @"mm\:ss";

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
            var str = TimeSpan.FromSeconds(time).ToString(format);
            GetSelf<EETextSimple>().SetData(str);
        }
    }
}
