using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RectTransformApproachSize : EEBehaviourLoop
    {
        public RectTransform RectTarget;
        public Vector2Approach Vector2Approach;
        
        protected override void Loop()
        {
            base.Loop();
            Vector2Approach.SetCurrent(GetSelf<RectTransform>().sizeDelta);
            if (RectTarget) Vector2Approach.SetTarget(RectTarget.sizeDelta);
            Vector2Approach.Proceed();
            GetSelf<RectTransform>().sizeDelta = Vector2Approach.Current;
        }

        public void SetRectTarget(RectTransform rectTransform)
        {
            RectTarget = rectTransform;
        }

        public void On()
        {
            Loop();
            Vector2Approach.On();
        }
        
        public void Off()
        {
            Vector2Approach.Off();
        }
    }
}
