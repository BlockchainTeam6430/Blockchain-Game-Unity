using System.Collections.Generic;
using LiteNetLib;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.LiteNet
{
    public class LiteNetPeer : EEPeer
    {
        public NetPeer NetPeer;
        private static readonly Dictionary<NetPeer, LiteNetPeer> peers = new Dictionary<NetPeer, LiteNetPeer>();

        public void Set(NetPeer netPeer)
        {
            NetPeer = netPeer;
            ID = netPeer.Id.ToString();
        }
        
        public static EEPeer Convert(NetPeer netPeer)
        {
            if (peers.TryGetValue(netPeer, out var peer)) return peer;
            peer = new LiteNetPeer();
            peer.Set(netPeer);
            peers.Add(netPeer, peer);
            return peer;
        }
        
        public static void Kick(NetPeer netPeer)
        {
            peers.Remove(netPeer);
        }
    }
}
