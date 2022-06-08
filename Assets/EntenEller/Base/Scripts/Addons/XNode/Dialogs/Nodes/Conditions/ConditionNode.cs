using Plugins.EntenEller.Base.Scripts.Addons.XNode;

// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog
{
    [NodeTint("#D6C7B4")]
    public abstract class ConditionNode : EEOutputNode
    {
        public abstract bool GetResult();
    }
}