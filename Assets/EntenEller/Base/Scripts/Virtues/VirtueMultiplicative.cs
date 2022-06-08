namespace Plugins.EntenEller.Base.Scripts.Virtues
{
    public class VirtueMultiplicative : Virtue
    {
        public override float Summary(float previousValue)
        {
            return previousValue * Value;
        }
    }
}
