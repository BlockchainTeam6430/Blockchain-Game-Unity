using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Character;
using Project.Scripts.Network.Lobby;

namespace Project.Scripts.Network
{
    public class PlayersReadyManager : EEBehaviour, IEENetReceiverServer
    {
        public EESuperAction StartGameSuperAction;
        public static Dictionary<EEPeer, SubserverData> peers = new Dictionary<EEPeer, SubserverData>();

        public void ReceiveAsServer(EEPeer peer, object data)
        {
            if (peers.ContainsKey(peer)) return;
            if (!(data is SubserverData)) return;
            var subserverData = (SubserverData) data;
          /*  if (subserverData.PasswordsTemp.Count > 2) return;
            var isValid = false;
            foreach (var password in subserverData.PasswordsTemp)
            {
                if (!LobbySubserver.SubserverData.PasswordsTemp.Contains(password)) continue;
                LobbySubserver.SubserverData.PasswordsTemp.Remove(password);
                isValid = true;
                break;
            }
            subserverData.NFTURL = "https://api.hypehippo.io/images/" + subserverData.NFTURL;
            if (!isValid) return; */
            switch (LobbySubserver.SubserverData.MaxPlayers)
            {
                case 1:
                {
                    var obj = GetChild<EESpawner>().Spawn().GetSelf<EENetPoolObject>();
                    obj.GetSelf<PlayerOwner>().SetPeer(peer, subserverData);
                    EEPacketToClient.Send(peer, typeof(CharacterIcon), subserverData.NFTURL, obj);
                    StartGameSuperAction.Call();
                    break;
                }
                case 2:
                {
                    peers.Add(peer, subserverData);
                    if (peers.Count == 2)
                    {
                        var obj1 = GetChild<EESpawner>().Spawn().GetSelf<EENetPoolObject>();
                        var obj2 = GetChild<EESpawner>().Spawn().GetSelf<EENetPoolObject>();
                        
                        obj1.GetSelf<PlayerOwner>().SetPeer(peers.First().Key, peers.First().Value);
                        obj2.GetSelf<PlayerOwner>().SetPeer(peers.Last().Key, peers.First().Value);
                        
                       // EEPacketToClient.Send(peers.First().Key, typeof(CharacterIcon), peers.First().Value.NFTURL, obj1);
                       // EEPacketToClient.Send(peers.First().Key, typeof(CharacterIcon), peers.Last().Value.NFTURL, obj2);
                        
                       // EEPacketToClient.Send(peers.Last().Key, typeof(CharacterIcon), peers.First().Value.NFTURL, obj2);
                      //  EEPacketToClient.Send(peers.Last().Key, typeof(CharacterIcon), peers.Last().Value.NFTURL, obj1);
                        StartGameSuperAction.Call();
                    }
                    break;
                }
            }
        }
    }
}
