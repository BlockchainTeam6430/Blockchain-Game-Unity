using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEditor;
using UnityEngine;
using TransformUtils = Plugins.EntenEller.Base.Scripts.Advanced.Transforms.TransformUtils;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public abstract class EEInfinityScroll : EEBehaviourEEMenu
    {
        [SerializeField] public float Offset;
        [SerializeField] public float Spacing;
        private List<RectTransform> children;
        private Vector2 firstPosition;

        protected override void EEAwake()
        {
            base.EEAwake();
            children = TransformUtils.GetFirstRowOfChildren(transform).Select(a => a.GetComponent<RectTransform>()).ToList();
            firstPosition = children.First().anchoredPosition;
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<RectTransformPositionObserver>().ChangedEvent += Changed;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<RectTransformPositionObserver>().ChangedEvent -= Changed;
        }

        public abstract void Changed(Vector3 delta);

        public void Restart()
        {
            children.ForEach(a => a.SetAsLastSibling());
            children.First().anchoredPosition = firstPosition;
            GetSelf<RectTransform>().anchoredPosition = Vector3.zero;
            GetSelf<RectTransformApproachPosition>().Position.SetCurrent(Vector2.zero);
            GetSelf<RectTransformApproachPosition>().Position.SetTarget(Vector2.zero);
            GetSelf<RectTransformPositionObserver>().Restart();
        }
    }
}
