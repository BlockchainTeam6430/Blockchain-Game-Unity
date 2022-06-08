using System.Collections.Generic;
using System.Linq;
using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter
{
    public abstract class DialogNodeSorter : EEReadyBehaviour
    {
        public List<DialogNode> DialogNodes;

        protected void Sort(DialogNode inputNode)
        {
            DialogNodes = EEAdvancedGraph.ChooseNodesOfType<DialogNode>(GetParent<DialogGraphLoader>().DialogNodes);

            RemoveWrongInputs();
            RemoveWrongConditions();
            SortByYPosition();
            AdditionalSort();
            Ready();

            void RemoveWrongInputs()
            {
                DialogNodes.RemoveAll(a =>
                {
                    var connectedNodes = a.GetListOfConnectedNodes(nameof(a.Input));
                    if (connectedNodes.Count == 0 && inputNode is null) return false;
                    return connectedNodes.FirstOrDefault(b => b == inputNode) is null;
                });
            }

            void RemoveWrongConditions()
            {
                DialogNodes.RemoveAll(a => DialogValidator.ValidateDialogLineNode(a) == false);
            }

            void SortByYPosition()
            {
                DialogNodes = DialogNodes.OrderBy(a => a.position.y).ToList();
            }
        }

        protected abstract void AdditionalSort();
    }
}
