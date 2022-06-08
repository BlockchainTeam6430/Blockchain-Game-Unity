using System;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class Vector3Approach : VariableApproach
    {
        public Vector3 Target;
        public Vector3 Current;
        
        protected override void GetNewValue()
        {
            Current = Approach.Style switch
            {
                ApproachStyle.Speed => Vector3.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                ApproachStyle.Lerp => Vector3.Lerp(Current, Target, Approach.Lerp),
                ApproachStyle.Instant => Target,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public void SetTarget(Vector3 target)
        {
            Target = target * OffsetFactor;
            On();
        }
        
        protected override bool CheckIfNear()
        {
            return Current.IsAlmostEqual(Target, ApproximateError);
        }
    }
}