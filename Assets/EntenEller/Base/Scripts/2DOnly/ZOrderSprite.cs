using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts._2DOnly
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class ZOrderSprite : ZOrderBase
    {
        private int highestZOrder = 32000;
        
        protected override void SetZ()
        {
            GetSelf<SpriteRenderer>().sortingOrder = highestZOrder - (int) (GetSelf<Transform>().position.y * 100);
        }
    }
}
