using System;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class FloatApproach : VariableApproach
    {
        public float Target;
        public float Current;

        protected override void GetNewValue()
        {
            Current = Approach.Style switch
            {
                ApproachStyle.Speed => Mathf.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                ApproachStyle.Lerp => Mathf.Lerp(Current, Target, Approach.Lerp),
                ApproachStyle.Instant => Target,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetTarget(float target)
        {
            Target = target * OffsetFactor;
            On();
        }

        public void SetCurrent(float current)
        {
            Current = current;
        }
        
        protected override bool CheckIfNear()
        {
            var isEqual = Current.IsAlmostEqual(Target, ApproximateError);
            if (isEqual)
            {
                Current = Target;
            }
            return isEqual;
        }
    }
}