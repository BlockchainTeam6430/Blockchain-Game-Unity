using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Blockchain;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Network.Lobby
{
    public class LobbyClient : EEBehaviour, IEENetReceiverClient
    {
        public static SubserverData SubserverData;
        [SerializeField] private EESuperAction ReadyToPlay;
        
        [DllImport("__Internal")]
        private static extern void EEPVPModeEntered();
        
        [DllImport("__Internal")]
        private static extern bool EEPVPModeIsReady();
        
        public void Solo()
        {
            EEPacketToServer.Send(typeof(LobbyServerClients), 1);
        }

        public async void PVP()
        {
            #if UNITY_WEBGL
            while (EEPVPModeIsReady())
            {
                await new WaitForSeconds(1f);
            }
            #endif
            EEPacketToServer.Send(typeof(LobbyServerClients), 2);
            string room = await ContractController.CurrentRoom();
            if(room != "")
            {
                ContractController.JoinRoom(room);
            }else
            {
                ContractController.CreateRoom();
            }

        }

        public void ReceiveAsClient(object data)
        {
            GetSelf<WebsocketClient>().Off();
            SubserverData = new SubserverData();
            var list = (List<string>)data;
            SubserverData.URL = list[0];
            SubserverData.PasswordsTemp.Add(list[1]);
            SubserverData.NFTURL = NFTImageToggle.CurrentID;
            SubserverData.Wallet = Wallet.Account;
            ReadyToPlay.Call();
        }
    }
}
