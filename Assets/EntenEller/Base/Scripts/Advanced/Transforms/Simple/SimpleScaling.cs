using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.SimpleActions
{
    public class SimpleScaling : EEBehaviourLoop
    {
        public Vector3 Scale;
        public float Speed;
        
        protected override void Loop()
        {
            base.Loop();
            GetSelf<Transform>().localScale = GetSelf<Transform>().localScale + Scale * (Speed * Time.deltaTime);
        }

        public void ChangeSpeed(float speedNew)
        {
            Speed = speedNew;
        }
        
        public void ChangeScale(Vector3 scaleNew)
        {
            Scale = scaleNew;
        }
    }
}
