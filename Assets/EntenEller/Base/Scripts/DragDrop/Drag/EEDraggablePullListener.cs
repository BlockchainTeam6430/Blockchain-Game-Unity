using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public abstract class EEDraggablePullListener : EEBehaviour
    {
        public EESuperAction DragStartSuperAction;
        public EESuperAction DragStopSuperAction;

        protected void DragStart()
        {
            DragStartSuperAction.Call(ComponentID);
        }

        protected void DragEnd()
        {
            DragStopSuperAction.Call(ComponentID);
        }
    }
}
