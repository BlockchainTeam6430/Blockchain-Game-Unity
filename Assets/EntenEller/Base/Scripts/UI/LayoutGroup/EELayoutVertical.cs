using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.LayoutGroup
{
    public class EELayoutVertical : EELayout
    {
        protected override void Changed()
        {
            base.Changed();
            foreach (var child in Children)
            {
                child.anchorMin = new Vector2(child.anchorMin.x,1);
                child.anchorMax = new Vector2(child.anchorMax.x,1);
                child.pivot = new Vector2(child.pivot.x, 1);
            }
            CountSize();
        }

        protected override float GetSize(RectTransform rectTransform)
        {
            return rectTransform.sizeDelta.y;
        }

        protected override void UpdatePositions(RectTransform rectTransform, float position)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, position);
        }
    }
}
