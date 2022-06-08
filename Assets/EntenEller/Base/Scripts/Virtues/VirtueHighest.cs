using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Virtues
{
    public class VirtueHighest : Virtue
    {
        public override float Summary(float previousValue)
        {
            return previousValue > Mathf.Abs(Value) ? previousValue : Value;
        }
    }
}
