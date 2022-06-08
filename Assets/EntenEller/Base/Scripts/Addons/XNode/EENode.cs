using System.Collections.Generic;
using System.Linq;
using XNode;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode
{
    public abstract class EENode : Node
    {
        public List<EENode> GetListOfConnectedNodes(string name)
        {
            return GetInputValues<EENode>(name).ToList();
        }
    }
}