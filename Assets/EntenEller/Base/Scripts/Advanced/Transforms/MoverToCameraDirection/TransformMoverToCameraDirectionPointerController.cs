using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.MoverToCameraDirection
{
    public class TransformMoverToCameraDirectionPointerController : EEBehaviour
    {
        [SerializeField] private bool is2D;
        [SerializeField] private float factor;
        
        protected override void EEEnable()
        {
            base.EEEnable();
          //  GetSelf<EEPointerTarget>().PointersData.First().DragEvent += Drag;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
           // GetSelf<EEPointerTarget>().PointersData.First().DragEvent -= Drag;
        }
        
        private void Drag(Vector2 delta)
        {
            delta *= factor;
            if (is2D)
            {
                GetParent<TransformMoverToCameraDirection>().Move(new Vector2(-delta.x, -delta.y));
            }
            else GetParent<TransformMoverToCameraDirection>().Move(new Vector3(-delta.x, 0, -delta.y));
        }
    }
}
