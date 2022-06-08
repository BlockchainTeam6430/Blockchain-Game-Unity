using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    [ExecutionOrder(9999)]
    public class TransformPositionObserver : TransformPositionObserverBase
    {
        protected override void Loop()
        {
            base.Loop();
            Last = Current;
            Current = GetSelf<Transform>().position;
        }
    }
}
