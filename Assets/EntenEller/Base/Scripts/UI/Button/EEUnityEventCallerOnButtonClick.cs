using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.UI.Button
{
    public class EEUnityEventCallerOnButtonClick : EEUnityEvent
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetParent<EEButton>().SuperAction.Event += Call;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetParent<EEButton>().SuperAction.Event -= Call;
        }
    }
}
