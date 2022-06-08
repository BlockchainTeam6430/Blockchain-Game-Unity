using System;
using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Nodes;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem
{
    public class Dialog : EEBehaviour
    {
        public event Action<DialogGraph> DialogStartedEvent;
        public event Action<DialogPlayerNode> DialogContinuedEvent;
        public event Action DialogEndedEvent;

        public void StartDialog(DialogGraph dialogGraph)
        {
            EEDebug.Log(EEDebugTag.Dialog, "Started " + dialogGraph.name);
            ActionUtils.Call(DialogStartedEvent, dialogGraph);
            ContinueDialog(null);
        }

        public void ContinueDialog(DialogPlayerNode dialogPlayerNode)
        {
            ActionUtils.Call(DialogContinuedEvent, dialogPlayerNode);
        }

        public void EndDialog()
        {
            ActionUtils.Call(DialogEndedEvent);
        }
    }
}