using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RectTransformApproachPosition : EEBehaviourLoop
    {
        public RectTransform RectTarget;
        public Vector2Approach Position;
        
        protected override void Loop()
        {
            base.Loop();
            Position.SetCurrent(GetSelf<RectTransform>().anchoredPosition);
            if (RectTarget) Position.SetTarget(RectTarget.anchoredPosition);
            Position.Proceed();
            GetSelf<RectTransform>().anchoredPosition = Position.Current;
        }

        public void SetRectTarget(RectTransform rectTransform)
        {
            RectTarget = rectTransform;
        }

        public void On()
        {
            Loop();
            Position.On();
        }
        
        public void Off()
        {
            Position.Off();
        }
    }
}
