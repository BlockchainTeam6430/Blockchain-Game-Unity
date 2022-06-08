using Plugins.EntenEller.Base.Scripts.Addons.XNode;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog
{
    [NodeWidth(500)]
    public abstract class DialogNode : EEOutputNode
    {
        [TextArea(10, 10)] public string Text;
        public AudioClip AudioClip;
        [PropertySpace(SpaceAfter = 10)][Input(backingValue = ShowBackingValue.Never)] public DialogNode Input;
        [Input(backingValue = ShowBackingValue.Never)] public ConditionNode ConditionNodes;
        [Input(backingValue = ShowBackingValue.Never)] public ActionNode OnEnableActionNodes;
        [PropertySpace(SpaceBefore = 10)][Input(backingValue = ShowBackingValue.Never)][PropertyOrder(1)] public EEOutputNode MiscNodes;
        
        public override object GetValue(NodePort port)
        {
            return this;
        }
    }
}