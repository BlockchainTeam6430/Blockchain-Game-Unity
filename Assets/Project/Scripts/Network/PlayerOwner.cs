using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Affiliation;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Network.Lobby;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class PlayerOwner : EEBehaviour, IEENetReceiverClient
    {
        [HideInInspector] public EEPeer Peer;
        public SubserverData SubserverData;
        public bool IsMine;
        public bool IsDead;
        
        public void SetPeer(EEPeer peer, SubserverData subserverData)
        {
            SubserverData = subserverData;
            Peer = peer;
            EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(PlayerOwner), a == peer, GetSelf<EENetPoolObject>()));
        }

        public void ReceiveAsClient(object data)
        {
            var isMine = (bool) data;
            IsMine = isMine;
            if (!isMine) EETagUtils.FindEETagInScenes("2nd-player-bars").GetSelf<EEGameObject>().SetState(true);
        }
    }
}
