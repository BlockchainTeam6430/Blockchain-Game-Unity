using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI
{
    public static class EERectTransformUtils
    {
        public static Vector2 GetSizeWithScale (this RectTransform target)
        {
            return target.sizeDelta * target.localScale;
        }
    }
}
