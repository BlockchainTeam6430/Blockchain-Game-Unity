using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI
{
    public class DialogUITextNpc : EEReadyBehaviour
    {
        [SerializeField] private EETextSimple text = null;

        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<DialogNodeSorterNpc>().ReadySuperAction.Event += ReadyDialog;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            GetSelf<DialogNodeSorterNpc>().ReadySuperAction.Event -= ReadyDialog;
        }

        private void ReadyDialog()
        {
            text.SetData(GetSelf<DialogNodeSorterNpc>().DialogNpcNode.Text);
            Ready();
        }
    }
}