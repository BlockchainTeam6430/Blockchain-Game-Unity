using NodeSystem.Dialog;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter
{
    public class DialogNodeSorterPlayer : DialogNodeSorter
    {
        protected override void EEAwake()
        {
            GetNeighbor<DialogNodeSorterNpc>().ReadySuperAction.Event += () =>
            {
                Sort(GetNeighbor<DialogNodeSorterNpc>().DialogNpcNode);
            };
        }

        protected override void AdditionalSort()
        {
            DialogNodes.RemoveAll(a => !(a is DialogPlayerNode));
            if (DialogNodes.Count == 0) AddEndingNode();

            void AddEndingNode()
            {
                DialogNodes.Add(ScriptableObject.CreateInstance<DialogPlayerNode>());
            }
        }
    }
}