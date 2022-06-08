using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Advanced.Triggers;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.DragDrop.Drag;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drop
{
    public class EEDropZone : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            EEDraggable.StopDragAnyEvent += Stop;
        }
        
        protected override void EEDisable()
        {
            base.EEDisable();
            EEDraggable.StopDragAnyEvent -= Stop;
        }

        private void Stop()
        {
            var collisions = GetNeighbor<EETrigger>().RightCollisions;
            var children = GetAllChild<Transform>();
            
            if (collisions.Count > children.Length)
            {
                for (var i = 0; i < collisions.Count; i++)
                {
                    var trigger = collisions[i];
                    if (i == 0)
                    {
                        trigger.GetNeighbor<EEDraggableRetreater>().Back();
                        continue;
                    }
                    var child = children[i - 1];
                    trigger.GetParent<TransformApproachPRS>().SetTarget(child);
                }
                return;
            }
            
            for (var i = 0; i < collisions.Count; i++)
            {
                var trigger = collisions[i];
                var child = children[i];
                trigger.GetParent<TransformApproachPRS>().SetTarget(child);
            }
        }
    }
}
