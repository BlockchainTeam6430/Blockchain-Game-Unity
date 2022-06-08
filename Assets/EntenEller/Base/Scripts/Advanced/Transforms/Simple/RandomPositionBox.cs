using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RandomPositionBox : RandomPositionBase
    {
        public override (Vector3 min, Vector3 max) GetBounds()
        {
            var obj = Target.GetSingle(this);
            var box = GetSelf<BoxCollider>();
            
            var pos = obj.transform.position;
            
            var min = pos + box.center;
            var max = min;
            
            min.x -= box.size.x / 2;
            min.y -= box.size.y / 2;
            min.z -= box.size.z / 2;
            
            max.x += box.size.x / 2;
            max.y += box.size.y / 2;
            min.z -= box.size.z / 2;
            
            return (min, max);
        }
    }
}
