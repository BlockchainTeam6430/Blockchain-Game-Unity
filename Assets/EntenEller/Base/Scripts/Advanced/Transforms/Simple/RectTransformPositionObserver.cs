using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RectTransformPositionObserver : TransformPositionObserverBase
    {
        protected override void Loop()
        {
            base.Loop();
            Last = Current;
            Current = GetSelf<RectTransform>().anchoredPosition;
        }
        
        public void Restart()
        {
            Current = GetSelf<RectTransform>().anchoredPosition;
        }
    }
}
