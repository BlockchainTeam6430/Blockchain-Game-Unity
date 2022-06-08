using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public class EEInfinityScrollX : EEInfinityScroll
    {
        public override void Changed(Vector3 delta)
        {
            var direction = Mathf.Sign(delta.x);

            var min = GetSelf<Transform>().GetChild(0).GetComponent<RectTransform>();
            var max = GetSelf<Transform>().GetChild(GetSelf<Transform>().childCount - 1).GetComponent<RectTransform>();
            
            if (direction > 0)
            {
                var pos = max.position / GetParent<EEMenu>().GetSelf<Transform>().localScale.x;
                if (pos.x < GetParent<EEMenu>().GetSelf<RectTransform>().sizeDelta.x + Offset) return;
                var x = min.anchoredPosition.x;
                x -= min.sizeDelta.x / 2;
                x -= Spacing;
                x -= max.sizeDelta.x / 2; 
                max.anchoredPosition = new Vector2(x, max.anchoredPosition.y);
                max.SetAsFirstSibling();
            }
            else
            {
                var pos = min.position / GetParent<EEMenu>().GetSelf<Transform>().localScale.x;
                if (pos.x > -Offset) return;
                var x = max.anchoredPosition.x;
                x += max.sizeDelta.x / 2;
                x += Spacing;
                x += min.sizeDelta.x / 2;
                min.anchoredPosition = new Vector2(x, min.anchoredPosition.y);
                min.SetAsLastSibling();
            }
        }
    }
}
