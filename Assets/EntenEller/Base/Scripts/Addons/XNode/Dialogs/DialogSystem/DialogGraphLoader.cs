using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem
{
    public class DialogGraphLoader : EEBehaviour
    {
        public List<EENode> DialogNodes { get; private set; }

        protected override void EEAwake()
        {
            GetSelf<Dialog>().DialogStartedEvent += dialogGraph =>
            {
                EEDebug.Log(EEDebugTag.Dialog, "Loaded " + dialogGraph.name);
                DialogNodes = EEAdvancedGraph.GetAdvancedNodes(dialogGraph);
            };
        }
    }
}