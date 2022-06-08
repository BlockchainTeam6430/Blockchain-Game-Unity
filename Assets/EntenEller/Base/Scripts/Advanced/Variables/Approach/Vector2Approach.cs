using System;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    [Serializable]
    public class Vector2Approach : VariableApproach
    {
        public Vector2 Target;
        public Vector2 Current;
        
        protected override void GetNewValue()
        {
            Current = Approach.Style switch
            {
                ApproachStyle.Speed => Vector2.MoveTowards(Current, Target, Approach.Speed * Time.deltaTime),
                ApproachStyle.Lerp => Vector2.Lerp(Current, Target, Approach.Lerp),
                ApproachStyle.Instant => Target,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public void SetTarget(Vector2 target)
        {
            Target = target * OffsetFactor;
            On();
        }
        
        public void SetCurrent(Vector2 current)
        {
            Current = current;
        }
        
        protected override bool CheckIfNear()
        {
            return Current.IsAlmostEqual(Target, ApproximateError);
        }
    }
}
