using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Sirenix.OdinInspector;

namespace Plugins.EntenEller.Base.Scripts.Network.Net
{
    public class EEServerPeerManager : EEBehaviour
    {
        [ReadOnly] public List<EEPeer> Peers = new List<EEPeer>();
        
        protected override void EEEnable()
        {
            base.EEEnable();
            EEServerGlobalEvents.ClientConnectedEvent += Connected;
            EEServerGlobalEvents.ClientDisconnectedEvent += Disconnected;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EEServerGlobalEvents.ClientConnectedEvent -= Connected;
            EEServerGlobalEvents.ClientDisconnectedEvent -= Disconnected;
        }

        private void Connected(EEPeer peer)
        {
            Peers.Add(peer);
        }

        private void Disconnected(EEPeer peer)
        {
            Peers.Remove(peer);
        }
    }
}
