using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach
{
    public abstract class VariableApproach
    {
        public bool IsActive = false;
        
        [ReadOnly] public bool IsReached = true;
        
        [SerializeField] protected float ApproximateError = EEConstants.MeasurementAccuracyHigh;
        [SerializeField] protected float OffsetFactor = 1;
        public Approach Approach = new Approach();
        
        public EESuperAction StartSuperAction = new EESuperAction();
        public EESuperAction ReachedSuperAction = new EESuperAction();
        public EESuperAction StopSuperAction = new EESuperAction();

        public void Proceed()
        {
            if (!IsActive) return;
            if (CheckIfNear())
            {
                if (!IsReached) ReachedSuperAction.Call();
                IsReached = true;
                return;
            }
            IsReached = false;
            GetNewValue();
        }

        protected abstract bool CheckIfNear();
        
        protected abstract void GetNewValue();

        public void On()
        {
            if (CheckIfNear()) return;
            StartSuperAction.Call();
            IsReached = false;
            IsActive = true;
        }
        
        public void Off()
        {
            StopSuperAction.Call();
            IsActive = false;
        }
    }
    
    public enum ApproachStyle
    {
        Lerp,
        Speed,
        Instant
    }
    
    [Serializable]
    public class Approach
    {
        public ApproachStyle Style = ApproachStyle.Speed;
        [ShowIf("Style", ApproachStyle.Lerp)] public float Lerp = 0.1f;
        [ShowIf("Style", ApproachStyle.Speed)] public float Speed = 1f;

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetLerp(float lerp)
        {
            Lerp = lerp;
        }
    }
}
