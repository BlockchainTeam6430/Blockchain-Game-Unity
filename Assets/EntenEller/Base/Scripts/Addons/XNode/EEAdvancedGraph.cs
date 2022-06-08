using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Nodes;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode
{
    public static class EEAdvancedGraph
    {
        public static List<EENode> GetAdvancedNodes(DialogGraph dialogGraph)
        {
            return dialogGraph.nodes.Select(node => node as EENode).ToList();
        }

        public static List<T> ChooseNodesOfType<T>(List<EENode> list) where T : EENode
        {
             return list.OfType<T>().ToList();
        }
    }
}