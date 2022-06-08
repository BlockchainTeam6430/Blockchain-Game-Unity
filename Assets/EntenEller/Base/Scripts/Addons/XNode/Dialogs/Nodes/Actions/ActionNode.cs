using Plugins.EntenEller.Base.Scripts.Addons.XNode;

// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog
{ 
    [NodeTint("#F9C53B")]
    public abstract class ActionNode : EEOutputNode
    {
        public abstract void Call();
    }
}