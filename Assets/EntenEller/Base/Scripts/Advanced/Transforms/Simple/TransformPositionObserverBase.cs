using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public abstract class TransformPositionObserverBase : EEBehaviourLoop
    {
        public Vector3 Current;
        public Vector3 Last;
        public Vector3 Delta;
        public EESuperAction ChangedSuperAction;
        public Action<Vector3> ChangedEvent;
        public bool IsMoving;

        protected override void Loop()
        {
            base.Loop();
            Delta = Current - Last;
            if (!Delta.sqrMagnitude.IsAlmostZero(EEConstants.MeasurementAccuracyMax))
            {
                IsMoving = true;
                ChangedEvent.Call(Delta);
                ChangedSuperAction.Call(ComponentID);
            }
            else IsMoving = false;
        }
    }
}
