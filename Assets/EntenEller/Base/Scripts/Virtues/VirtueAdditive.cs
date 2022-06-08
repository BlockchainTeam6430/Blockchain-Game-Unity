namespace Plugins.EntenEller.Base.Scripts.Virtues
{
    public class VirtueAdditive : Virtue
    {
        public override float Summary(float previousValue)
        {
            return previousValue + Value;
        }
    }
}
