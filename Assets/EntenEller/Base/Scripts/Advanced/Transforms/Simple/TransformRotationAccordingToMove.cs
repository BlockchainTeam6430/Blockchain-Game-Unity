using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions
{
    public class TransformRotationAccordingToMove : EEBehaviourLoop
    {
        public Axis Axe;
        public Vector3 Rotation = new Vector3(0, 0, 0);
        
        protected override void Loop()
        {
            base.Loop();
            var delta = GetSelf<TransformPositionObserver>().Delta;
            var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            switch (Axe)
            {
                case Axis.x:
                    Rotation.x = angle;
                    break;
                case Axis.y:
                    Rotation.y = angle;
                    break;
                case Axis.z:
                    Rotation.z = angle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            GetSelf<TransformApproachRotation>().Rotation.SetTarget(Quaternion.Euler(Rotation));
        }

        public enum Axis
        {
            x,y,z
        }
    }
}
