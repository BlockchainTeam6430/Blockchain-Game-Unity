using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public class EEScrollPRS : EEBehaviourEEMenu
    {
        private Vector2 startingPosition;

        protected override void EEEnable()
        {
            base.EEEnable();
            var first = GetSelf<Transform>().GetChild(0).GetComponent<RectTransform>();
            startingPosition = first.anchoredPosition;
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            var first = GetSelf<Transform>().GetChild(0).GetComponent<RectTransform>();
            var x = first.anchoredPosition.x + first.GetSizeWithScale().x / 2;
            for (var i = 1; i < GetSelf<Transform>().childCount; i++)
            {
                var rectTransform = GetSelf<Transform>().GetChild(i).GetComponent<RectTransform>();
                var halfSize = rectTransform.GetSizeWithScale().x / 2;
                x += halfSize + GetSelf<EEInfinityScroll>().Spacing;
                rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
                x += halfSize;
            }
        }

        public void Restart()
        {
            
            var first = GetSelf<Transform>().GetChild(0).GetComponent<RectTransform>();
        }
    }
}
