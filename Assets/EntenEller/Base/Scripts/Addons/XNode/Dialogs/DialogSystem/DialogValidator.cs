using System.Linq;
using NodeSystem.Dialog;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem
{
    public static class DialogValidator
    {
        public static bool ValidateDialogLineNode<T>(T dialogLineNode) where T : DialogNode
        {
            var connectedNodes = dialogLineNode.GetListOfConnectedNodes(nameof(dialogLineNode.ConditionNodes));
            if (connectedNodes.Count == 0) return true;
            foreach (var conditionNode in connectedNodes.Select(node => node as ConditionNode))
            {
                return (bool) conditionNode.GetResult();
            }
            return true;
        }
    }
}
