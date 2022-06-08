using XNode;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode
{
    public abstract class EEOutputNode : EENode
    {
        [Output(backingValue = ShowBackingValue.Never)] public EENode Output;

        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}