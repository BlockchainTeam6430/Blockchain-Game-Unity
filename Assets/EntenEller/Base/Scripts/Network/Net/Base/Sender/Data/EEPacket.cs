using System;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data
{
    [Serializable]
    public class EEPacket
    {
        public int I, J;
        public string ScriptName;
        public bool IsForServer;
        public object Data;
    }
}