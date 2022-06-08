using Plugins.EntenEller.Base.Scripts.UI.Menu;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public class EEScrollMiddleScaler : EEBehaviourEEMenu
    {
        public float Offset;
        public float ScaleFactor;

        protected override void MenuLoop()
        {
            base.MenuLoop();
            var nearestElements = GetSelf<EEScrollMiddleObserver>().ScrollElements;
            foreach (var nearestElement in nearestElements)
            {
                var d = Mathf.Abs(nearestElement.Distance);
                d /= Offset;
                d = Mathf.Clamp(d, 0f, 1f);
                d = 1 - d;
                var v3 = new Vector3();
                v3.x = v3.y = v3.z = 1 + d * ScaleFactor;
                nearestElement.RectTransform.localScale = v3;
            }
        }
    }
}
