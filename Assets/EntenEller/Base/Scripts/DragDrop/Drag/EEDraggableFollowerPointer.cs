using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.DragDrop.Drag
{
    public class EEDraggableFollowerPointer : EEDraggableFollower
    {
        protected override Vector3 GetPosition()
        {
            return EESingleton.Get<EEPointerManager>().PointersData.First().Position;
        }
    }
}
