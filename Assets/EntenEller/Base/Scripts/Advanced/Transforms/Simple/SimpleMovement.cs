using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables;
using Project.Scripts.Network;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class SimpleMovement : EEBehaviourLoop
    {
        public Vector3 Direction;
        public FloatRandomOrDefined MinSpeed, Speed, MaxSpeed;
        public FloatRandomOrDefined Acceleration;
        
        protected override void Loop()
        {
            base.Loop();
            if (GameEnd.IsOver) return;
            Speed.SetValue(Speed.Value + Acceleration.Value * Time.deltaTime);
            
            if (Speed.Value > MaxSpeed.Value) Speed.SetValue(MaxSpeed.Value);
            else if (Speed.Value < MinSpeed.Value) Speed.SetValue(MinSpeed.Value);
            
            GetSelf<Transform>().localPosition = GetSelf<Transform>().localPosition + Direction * (Speed.Value * Time.deltaTime);
        }

        public void ChangeSpeed(float speed)
        {
            Speed.SetValue(speed);
        }
        
        public void ChangeMaxSpeed(float maxSpeed)
        {
            MaxSpeed.SetValue(maxSpeed);
        }
        
        public void ChangeAcceleration(float acceleration)
        {
            Acceleration.SetValue(acceleration);
        }
        
        public void ChangeDirection(Vector3 directionNew)
        {
            Direction = directionNew;
        }
    }
}
