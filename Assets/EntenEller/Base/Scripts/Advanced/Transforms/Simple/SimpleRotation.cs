using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions
{
    public class SimpleRotation : EEBehaviourLoop
    {
        public Vector3 Rotation;
        public float Speed;
        
        protected override void Loop()
        {
            base.Loop();
            GetSelf<Transform>().localEulerAngles = GetSelf<Transform>().localEulerAngles + Rotation * (Speed * Time.deltaTime);
        }

        public void ChangeSpeed(float speedNew)
        {
            Speed = speedNew;
        }
        
        public void ChangeRotation(Vector3 rotationNew)
        {
            Rotation = rotationNew;
        }
    }
}
