using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Actions
{
    public class DialogActionsOnEnable : EEBehaviour
    {
        protected override void EEAwake()
        {
            GetSelf<DialogNodeSorter>().ReadySuperAction.Event += () =>
            {
                GetSelf<DialogNodeSorter>().DialogNodes.ForEach(a =>
                {
                    var connectedNodes = a.GetListOfConnectedNodes(nameof(a.OnEnableActionNodes));
                    connectedNodes.ForEach(b => ((ActionNode) b).Call());
                });
            };
        }
    }
}
