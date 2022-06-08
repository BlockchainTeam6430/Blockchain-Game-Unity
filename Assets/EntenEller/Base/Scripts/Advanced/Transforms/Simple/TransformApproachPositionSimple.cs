namespace Plugins.EntenEller.Base.Scripts.Advanced.Transforms.Simple
{
    public class TransformApproachPositionSimple : TransformApproachPosition
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            Position.Current = IsGlobal ? CachedTransform.position : CachedTransform.localPosition;
        }

        protected override void GlobalMove()
        {
            CachedTransform.position = Position.Current;
        }

        protected override void LocalMove()
        {
            CachedTransform.localPosition = Position.Current;
        }
    }
}