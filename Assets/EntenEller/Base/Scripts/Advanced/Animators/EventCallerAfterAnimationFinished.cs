using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Animators
{
    public class EventCallerAfterAnimationFinished : EEUnityEvent
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEAnimatorUI>().FinishedSuperAction.Event += Call;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEAnimatorUI>().FinishedSuperAction.Event -= Call;
        }
    }
}
