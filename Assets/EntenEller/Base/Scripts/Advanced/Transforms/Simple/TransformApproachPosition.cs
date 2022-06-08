using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class TransformApproachPosition : TransformApproach
    {
        public Vector3Approach Position;
        
        protected override void Loop()
        {
            base.Loop();
            if (IsGlobal)
            {
                Position.Current = CachedTransform.position;
                if (Target) Position.Target = Target.position;
                Position.Proceed();
                GlobalMove();
            }
            else
            {
                Position.Current = CachedTransform.localPosition;
                if (Target) Position.Target = Target.localPosition;
                Position.Proceed();
                LocalMove();
            }
        }

        public override void On()
        {
            Position.On();
        }
        
        public override void Off()
        {
            Position.Off();
        }
        
        protected abstract void GlobalMove();
        protected abstract void LocalMove();
    }
}
