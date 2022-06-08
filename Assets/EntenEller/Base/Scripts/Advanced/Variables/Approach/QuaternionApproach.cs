using System;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class QuaternionApproach : VariableApproach
    { 
        public Quaternion Target;
        public Quaternion Current;
        
        protected override void GetNewValue()
        {
            Current = Approach.Style switch
            {
                ApproachStyle.Speed => Quaternion.RotateTowards(Current, Target, Approach.Speed * Time.deltaTime),
                ApproachStyle.Lerp => Quaternion.Lerp(Current, Target, Approach.Lerp),
                ApproachStyle.Instant => Target,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetTarget(Quaternion target)
        {
            Target = Quaternion.Slerp(Quaternion.identity, target, OffsetFactor);
            On();
        }

        protected override bool CheckIfNear()
        {
            return Quaternion.Angle(Target, Current).IsAlmostZero(ApproximateError);
        }
    }
}
