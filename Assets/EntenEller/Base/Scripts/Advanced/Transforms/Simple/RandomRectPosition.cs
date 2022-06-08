using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class RandomRectPosition : RandomPositionBase
    {
        public override (Vector3 min, Vector3 max) GetBounds()
        {
            var obj = Target.GetSingle(this);
            
            var rect = RectTransformUtility.PixelAdjustRect(obj.GetSelf<RectTransform>(), GetParent<Canvas>());
            var pos = (Vector2) obj.transform.position;

            var scale = obj.GetParent<Canvas>().transform.localScale;
            
            var min = pos + rect.center * scale;
            var max = min;

            var size = Vector2.zero;
            
            size.x = rect.width;
            size.y = rect.height;

            size *= scale;
            size /= 2;
            
            min.x -= size.x;
            min.y -= size.y;
            
            max.x += size.x;
            max.y += size.y;

            return (min, max);
        }
    }
}
