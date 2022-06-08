using System.Collections;
using Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.ScrollRect
{
    public class EEScrollStickerToMiddle : EEBehaviour
    {
        [SerializeField] private float minSpeedToStick;
        
        public void Stop()
        {
            GetSelf<RectTransformApproachPosition>().Off();
            StopAllCoroutines();
        }
        
        public void ScrollToElement(EEScrollElement scrollElement)
        {
            GetSelf<RectTransformApproachPosition>().On();
            StopAllCoroutines();
            StartCoroutine(ScrollRoutine(scrollElement));
        }

        private IEnumerator ScrollRoutine(EEScrollElement scrollElement)
        {
            while (true)
            {
                GetSelf<RectTransformApproachPosition>().Position.SetCurrent(GetSelf<RectTransform>().anchoredPosition);
                var pos = GetSelf<RectTransform>().anchoredPosition;
                pos.x += scrollElement.Distance;
                GetSelf<RectTransformApproachPosition>().Position.SetTarget(pos);
                yield return null;
            }
        }
        
        public bool IsReadyToStick()
        {
            var v = GetParent<UnityEngine.UI.ScrollRect>().velocity;
            var x = Mathf.Abs(v.x);
            var y = Mathf.Abs(v.y);
            var sum = x + y;
            return sum < minSpeedToStick;
        }
    }
}