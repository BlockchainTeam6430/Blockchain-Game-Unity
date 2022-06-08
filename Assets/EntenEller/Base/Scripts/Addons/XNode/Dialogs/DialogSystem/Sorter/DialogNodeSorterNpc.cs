using System;
using System.Linq;
using NodeSystem.Dialog;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter
{
    public class DialogNodeSorterNpc : DialogNodeSorter
    {
        public DialogNpcNode DialogNpcNode
        {
            get
            {
                if (!(DialogNodes.FirstOrDefault() is DialogNpcNode node)) throw new Exception("No NPC dialogs!");
                return node;
            }
        }

        protected override void EEAwake()
        {
            GetParent<Dialog>().DialogContinuedEvent += Sort;
        }

        protected override void AdditionalSort()
        {
            DialogNodes.RemoveAll(a => !(a is DialogNpcNode));
            DialogNodes.RemoveRange(1, DialogNodes.Count - 1);
        }
    }
}