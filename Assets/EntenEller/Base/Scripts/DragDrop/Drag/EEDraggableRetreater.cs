using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableRetreater : EEBehaviour
    {
        [SerializeField] private bool isSetPosition, isSetRotation, isSetScale;
        
        [ShowIf("isSetPosition")] public Vector3 Position;
        [ShowIf("isSetRotation")] public Quaternion Rotation;
        [ShowIf("isSetScale")] public Vector3 Scale;

        [SerializeField] private Approach positionApproach, rotationApproach, scaleApproach;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggableDropZoneFinder>().NoDraggablesToStickSuperAction.Event += Back;
            GetSelf<EEDraggablePullListener>().DragStartSuperAction.Event += DragStart;
        }

        private void DragStart()
        {
            GetSelf<EEDraggablePullListener>().DragStartSuperAction.Event -= DragStart;
            CachePRS();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggableDropZoneFinder>().NoDraggablesToStickSuperAction.Event -= Back;
        }

        public void Back()
        {
            GetParent<TransformApproachPRS>().SetApproachStyles(positionApproach, rotationApproach, scaleApproach);
            GetParent<TransformApproachPRS>().SetTarget(null);
            GetParent<TransformApproachPRS>().SetTarget(Position, Rotation, Scale);
        }

        public void CachePRS()
        {
            if (!isSetPosition) Position = GetParent<Transform>().position;
            if (!isSetRotation) Rotation = GetParent<Transform>().rotation;
            if (!isSetScale) Scale = GetParent<Transform>().lossyScale;
        }
    }
}
