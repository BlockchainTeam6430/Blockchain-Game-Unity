using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Virtues
{
    public abstract class Virtue : EEBehaviour
    {
        public int Rank = 0;
        public float Value;
        public bool IsPercent = false;

        public abstract float Summary(float previousValue);

        public void ChangeValue(float value)
        {
            Value = value;
        }
    }
}