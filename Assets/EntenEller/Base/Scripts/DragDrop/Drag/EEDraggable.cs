using System;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Plugins.EntenEller.Base.Scripts.Advanced.Triggers;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggable : EEBehaviourLoop
    {
        [ReadOnly] public bool IsDragging;
        [SerializeField] public bool LockedX = false;
        [SerializeField] public bool LockedY = false;
        [SerializeField] public bool IsStickWithoutPointerUp = false;
        
        public event Action<EEDraggable> StartDragEvent, StopDragEvent; 
        public static event Action StartDragAnyEvent, StopDragAnyEvent; 
        
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
            if (!IsStickWithoutPointerUp) return;
            if (!IsDragging) return;
            if (GetNeighbor<EETrigger>().RightCollisions.Count == 0) return;
            StopDrag();
        }

        private void StartDrag()
        {
            IsDragging = true;
            StartDragEvent.Call(this);
            StartDragAnyEvent.Call();
        }
        
        private void StopDrag()
        {
            IsDragging = false;
            StopDragEvent.Call(this);
            StopDragAnyEvent.Call();
        }
    }
}
