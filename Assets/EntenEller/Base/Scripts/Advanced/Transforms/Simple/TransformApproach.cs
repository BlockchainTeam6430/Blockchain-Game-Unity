using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class TransformApproach : EEBehaviourLoop
    {
        public Transform Target;
        public bool IsGlobal;

        private Transform _CachedTransform;

        protected Transform CachedTransform
        {
            get
            {
                if (_CachedTransform) return _CachedTransform;
                _CachedTransform = GetSelf<Transform>();
                return _CachedTransform;
            }
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            if (target.IsNull()) return;
            On();
        }

        public abstract void On();
        
        public abstract void Off();
    }
}
