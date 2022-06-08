using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.UI.LayoutGroup
{
    public class EELayoutGrid : EEBehaviour
    {
        [SerializeField] private bool isFlexibleChildWidth;
        [SerializeField] private bool isFlexibleChildHeight;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<EEGameObject>().ChangedHierarchySuperAction.Event += Changed;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<EEGameObject>().ChangedHierarchySuperAction.Event -= Changed;
        }

        private void Changed()
        {
            var count = GetSelf<GridLayoutGroup>().constraintCount;
            var w = GetSelf<GridLayoutGroup>().cellSize.x;
            var h = GetSelf<GridLayoutGroup>().cellSize.y;
            if (isFlexibleChildWidth)
            {
                w = GetSelf<RectTransform>().rect.width;
                w -= GetSelf<GridLayoutGroup>().padding.left + GetSelf<GridLayoutGroup>().padding.right + GetSelf<GridLayoutGroup>().spacing.x * (count - 1);
                w /= count;
            }
            if (isFlexibleChildHeight)
            {
                h = GetSelf<RectTransform>().rect.height;
                h -= GetSelf<GridLayoutGroup>().padding.top + GetSelf<GridLayoutGroup>().padding.bottom + GetSelf<GridLayoutGroup>().spacing.y * (count - 1);
                h /= count;
            }
            GetSelf<GridLayoutGroup>().cellSize = new Vector2(w, h);
        }
    }
}
