using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket
{
    public class WebsocketPeer : EEPeer
    {
        private static readonly Dictionary<string, WebsocketPeer> peers = new Dictionary<string, WebsocketPeer>();
        
        public void Set(string id)
        {
            ID = id;
        }
        
        public static EEPeer Convert(string id)
        {
            EEDebug.Log(id);
            if (peers.TryGetValue(id, out var peer)) return peer;
            peer = new WebsocketPeer();
            peer.Set(id);
            peers.Add(id, peer);
            return peer;
        }
        
        public static void Kick(string id)
        {
            peers.Remove(id);
        }
    }
}
