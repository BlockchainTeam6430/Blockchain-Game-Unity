using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public class EEScrollMiddleObserver : EEBehaviourEEMenu
    {
        [ReadOnly] public List<EEScrollElement> ScrollElements = new List<EEScrollElement>();
        private float middle;
        
        protected override void EEEnable()
        {
            base.EEEnable();
            var rects = transform.GetFirstRowOfChildren().Select(a => a.GetComponent<RectTransform>()).ToList();
            foreach (var rect in rects)
            {
                ScrollElements.Add(new EEScrollElement
                {
                    RectTransform = rect
                });
            }
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            var cs = GetParent<CanvasScaler>();
            middle = cs.referenceResolution.x / 2;
            var factor = cs.GetComponent<RectTransform>().sizeDelta.x / cs.referenceResolution.x;
            if (factor > 1) middle *= factor;
            var middlePos = -GetSelf<RectTransform>().anchoredPosition.x + middle;
            foreach (var nearestElement in ScrollElements)
            {
                nearestElement.Distance = middlePos - nearestElement.RectTransform.anchoredPosition.x;
            }
            ScrollElements = ScrollElements.OrderBy(a => Mathf.Abs(a.Distance)).ToList();
        }
    }
}
