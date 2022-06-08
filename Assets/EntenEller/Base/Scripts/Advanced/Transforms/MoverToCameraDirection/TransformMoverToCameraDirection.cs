using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.MoverToCameraDirection
{
    public class TransformMoverToCameraDirection : EEBehaviourLoop
    {
        [SerializeField] private MoveType moveType = MoveType.Lerp;
        [ShowIf("moveType", MoveType.Speed)]
        [SerializeField] private float speed = 1f;
        [ShowIf("moveType", MoveType.Lerp)]
        [SerializeField] private float lerp = 0.3f;
        [SerializeField] [ReadOnly] private Vector3 targetPosition;

        protected override void EEAwake()
        {
            base.EEAwake();
            targetPosition = GetSelf<Transform>().localPosition;
        }

        public void Move(Vector3 v3)
        {
            targetPosition = GetSelf<Transform>().localPosition + v3;
        }

        public void Move(Vector2 v2)
        {
            targetPosition = GetSelf<Transform>().localPosition + (Vector3) v2;
        }

        public void MoveX(float dx)
        {
            targetPosition = GetSelf<Transform>().localPosition;
            targetPosition.x += dx;
        }

        public void MoveY(float dy)
        {
            targetPosition = GetSelf<Transform>().localPosition;
            targetPosition.y += dy;
        }

        protected override void Loop()
        {
            base.Loop();
            switch (moveType)
            {
                case MoveType.Lerp:
                    GetSelf<Transform>().localPosition = Vector3.Lerp(GetSelf<Transform>().localPosition, targetPosition, lerp);
                    break;
                case MoveType.Speed:
                    GetSelf<Transform>().localPosition = Vector3.MoveTowards(GetSelf<Transform>().localPosition, targetPosition, speed * Time.deltaTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private enum MoveType
        {
            Speed,
            Lerp
        }
    }
}
