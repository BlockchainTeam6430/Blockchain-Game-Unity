using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class TransformApproachScale : TransformApproach
    {
        public Vector3Approach Scale;

        protected override void EEEnable()
        {
            base.EEEnable();
            Scale.Target = Scale.Current = IsGlobal ? CachedTransform.lossyScale : CachedTransform.localScale;
        }

        protected override void Loop()
        {
            base.Loop();
            if (IsGlobal)
            {
                var lossyBefore = CachedTransform.lossyScale;

                Scale.Current = lossyBefore;
                if (Target) Scale.Target = Target.lossyScale;
                
                Scale.Proceed();
                
                var lossyAfter = Scale.Current;
                var v3 = CachedTransform.localScale;

                v3.x *= lossyAfter.x / lossyBefore.x;
                v3.y *= lossyAfter.y / lossyBefore.y;
                v3.z *= lossyAfter.z / lossyBefore.z;
                
                CachedTransform.localScale = v3;
            }
            else
            {
                Scale.Current = CachedTransform.localScale;
                if (Target) Scale.Target = Target.localScale;
                Scale.Proceed();
                CachedTransform.localScale = Scale.Current;
            }
        }
        
        public override void On()
        {
            Scale.On();
        }
        
        public override void Off()
        {
            Scale.Off();
        }

        public void Set(float size)
        {
            Scale.SetTarget(new Vector3(size, size, size));
        }
    }
}
