using Plugins.EntenEller.Base.Scripts.Advanced.Triggers;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableDropZoneFinder : EEBehaviour
    {
        public EESuperAction NoDraggablesToStickSuperAction;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEDraggablePullListener>().DragStopSuperAction.Event += StopDrag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEDraggablePullListener>().DragStopSuperAction.Event -= StopDrag;
        }
        
        private void StopDrag()
        {
            if (GetNeighbor<EETrigger>().RightCollisions.Count != 0) return;
            NoDraggablesToStickSuperAction.Call(ComponentID);
        }
    }
}
