using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.LayoutGroup
{
    public abstract class EELayout : EEBehaviourEEMenu
    {
        public float OffsetBefore;
        public float SpaceBefore;
        public float SpaceBetween;
        public float SpaceAfter;
        
        protected List<RectTransform> Children;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<RectTransform>().anchorMin = new Vector2(0, 1);
            GetSelf<RectTransform>().anchorMax = new Vector2(1, 1);
            GetSelf<RectTransform>().pivot = new Vector2(0, 1);
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            gameObject.GetEEGameObject().ChangedHierarchySuperAction.Event += Changed;
            Changed();
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            gameObject.GetEEGameObject().ChangedHierarchySuperAction.Event -= Changed;
        }

        protected virtual void Changed()
        {
            Children = GetSelf<Transform>().GetFirstRowOfChildren().ConvertAll(a => a.GetComponent<RectTransform>());
            foreach (var rect in Children.ToList())
            {
                var tagHolder = rect.GetComponent<EETagHolder>();
                if (!tagHolder) continue;
                if (tagHolder.IsHavingTag("ignore-layout")) Children.Remove(rect);
            }
        }
        
        protected override void MenuLoop()
        {
            base.MenuLoop();
            CountSize();
        }

        public void CountSize()
        {
            var position = -SpaceBefore;
            foreach (var child in Children)
            {
                if (!child.gameObject.activeSelf) continue;
                if (Children.Count > 1) UpdatePositions(child, position);
                position -= GetSize(child) + SpaceBetween;
            }
            position -= SpaceAfter;
            GetSelf<RectTransform>().sizeDelta = new Vector2(0, -position);
        }

        protected abstract float GetSize(RectTransform rectTransform);

        protected abstract void UpdatePositions(RectTransform rectTransform, float position);
    }
}
