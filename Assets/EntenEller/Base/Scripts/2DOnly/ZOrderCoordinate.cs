using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts._2DOnly
{
    public class ZOrderCoordinate : ZOrderBase
    {
        protected override void SetZ()
        {
            var pos = GetSelf<Transform>().position;
            pos.z = pos.y;
            GetSelf<Transform>().position = pos;
        }
    }
}
