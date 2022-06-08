using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RandomPositionBox2D : RandomPositionBase
    {
        public override (Vector3 min, Vector3 max) GetBounds()
        {
            var obj = Target.GetSingle(this);
            var box = GetSelf<BoxCollider2D>();
            
            var pos = (Vector2) obj.transform.position;
            
            var min = pos + box.offset;
            var max = min;
            
            min.x -= box.size.x / 2;
            min.y -= box.size.y / 2;

            max.x += box.size.x / 2;
            max.y += box.size.y / 2;

            return (min, max);
        }
    }
}