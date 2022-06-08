using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.UI.Toggles
{
    public class EEToggleUnityEvent : EEUnityEventBinary
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetParent<EEToggle>().ValueIsOnSuperAction.Event += On;
            GetParent<EEToggle>().ValueIsOffSuperAction.Event += Off;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetParent<EEToggle>().ValueIsOnSuperAction.Event -= On;
            GetParent<EEToggle>().ValueIsOffSuperAction.Event -= Off;
        }

        private void On()
        {
            EventOn();
        }

        private void Off()
        {
            EventOff();
        }
    }
}
