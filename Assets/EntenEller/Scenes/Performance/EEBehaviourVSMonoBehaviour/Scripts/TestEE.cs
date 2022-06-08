using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Scenes.Performance.Base;

namespace Plugins.EntenEller.Scenes.Performance.EEBehaviourVSMonoBehaviour.Scripts
{
    public class TestEE : EEBehaviourLoop
    {
        protected override void Loop()
        {
            base.Loop();
            TestPerformance.Easy();
        }
    }
}
