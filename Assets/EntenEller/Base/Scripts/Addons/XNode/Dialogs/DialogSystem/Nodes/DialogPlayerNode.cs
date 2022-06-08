// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog
{ 
    [NodeTint("#7AB99C")]
    public class DialogPlayerNode : DialogNode
    {
        [Input(backingValue = ShowBackingValue.Never)] public ActionNode OnPressActionNodes;
    }
}