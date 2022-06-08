using Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggablePullListenerUI : EEDraggablePullListener
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetNeighbor<EEPointerUI>().DownSuperAction.Event += PointerDown;
            GetNeighbor<EEPointerUI>().UpSuperAction.Event += PointerUp;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetNeighbor<EEPointerUI>().DownSuperAction.Event -= PointerDown;
            GetNeighbor<EEPointerUI>().UpSuperAction.Event -= PointerUp;
        }
        
        protected void PointerUp()
        {
            if (GetSelf<EEDraggable>().IsDragging) DragEnd();
        }
        
        private void PointerDown()
        {
            DragStart();
        }
    }
}
