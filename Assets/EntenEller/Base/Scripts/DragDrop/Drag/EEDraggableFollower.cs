using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public abstract class EEDraggableFollower : EEBehaviourLoop
    { 
        [ReadOnly] public Vector3 Offset;
        [ReadOnly] public bool IsActive;
        [SerializeField] private Approach positionApproach, rotationApproach, scaleApproach;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggablePullListener>().DragStartSuperAction.Event += StartDrag;
            GetSelf<EEDraggablePullListener>().DragStopSuperAction.Event += StopDrag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggablePullListener>().DragStartSuperAction.Event -= StartDrag;
            GetSelf<EEDraggablePullListener>().DragStopSuperAction.Event -= StopDrag;
        }
        
        protected override void Loop()
        {
            base.Loop();
            if (!IsActive) return;
            var pos = GetPosition() + Offset;
            if (GetSelf<EEDraggable>().LockedX) pos.x = GetParent<TransformApproachPosition>().Position.Current.x;
            if (GetSelf<EEDraggable>().LockedY) pos.y = GetParent<TransformApproachPosition>().Position.Current.y;
            GetParent<TransformApproachPosition>().Position.SetTarget(pos);
        }

        private void StartDrag()
        {
            GetParent<TransformApproachPRS>().SetApproachStyles(positionApproach, rotationApproach, scaleApproach);
            GetParent<TransformApproachPRS>().SetTarget(null);
            Offset = GetParent<Transform>().position - GetPosition();
            IsActive = true;
        }
        
        private void StopDrag()
        {
            IsActive = false;
        }

        protected abstract Vector3 GetPosition();
    }
}