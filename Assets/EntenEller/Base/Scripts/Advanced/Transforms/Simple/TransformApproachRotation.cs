using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class TransformApproachRotation : TransformApproach
    {
        public QuaternionApproach Rotation;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            Rotation.Current = IsGlobal ? CachedTransform.rotation : CachedTransform.localRotation;
        }
        
        protected override void Loop()
        {
            base.Loop();
            if (IsGlobal)
            {
                Rotation.Current = CachedTransform.rotation;
                if (Target) Rotation.Target = Target.rotation;
                Rotation.Proceed();
                CachedTransform.rotation = Rotation.Current;
            }
            else
            {
                Rotation.Current = CachedTransform.localRotation;
                if (Target) Rotation.Target = Target.localRotation;
                Rotation.Proceed();
                CachedTransform.localRotation = Rotation.Current;
            }
        }
        
        public override void On()
        {
            Loop();
            Rotation.On();
        }
        
        public override void Off()
        {
            Rotation.Off();
        }
    }
}
