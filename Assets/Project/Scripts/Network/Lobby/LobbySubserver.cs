using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Files;
using Plugins.EntenEller.Base.Scripts.Advanced.Randoms;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using UnityEngine;
using WebSocketSharp;

namespace Project.Scripts.Network.Lobby
{
    public class LobbySubserver : EEBehaviourLoop, IEENetReceiverClient
    {
        public static SubserverData SubserverData = new SubserverData();
        private static string _URLPHP;
        private float lastDate = 60;
        
        public static string URLPHP
        {
            get
            {
                if (!_URLPHP.IsNullOrEmpty())
                {
                    return _URLPHP;
                }
                var url = System.Text.Encoding.Default.GetString(EEFileUtils.Load(Application.streamingAssetsPath + "/settings/subserver.txt"));
                _URLPHP = url.Replace(":", "");
                _URLPHP = _URLPHP.Replace("wss", "https:");
                _URLPHP += "/php/";
                return _URLPHP;
            }
        }
        
        public void Send()
        {
            #if !UNITY_WEBGL
                var url = System.Text.Encoding.Default.GetString(EEFileUtils.Load(Application.streamingAssetsPath + "/settings/subserver.txt"));
                SubserverData.URL =  url + GetSelf<WebsocketServer>().Port;
                GetSelf<EEHTTP>().url = URLPHP + "set.php?port=" + GetSelf<WebsocketServer>().Port;
                SubserverData.MaxPlayers = int.Parse(System.Text.Encoding.Default.GetString(EEFileUtils.Load(Application.streamingAssetsPath + "/settings/amount.txt")));
                for (var i = 0; i < SubserverData.MaxPlayers; i++)
                {
                    SubserverData.PasswordsTemp.Add(EERandomUtils.RandomAlphaNumericString(128));
                }
                SubserverData.PasswordVerification = System.Text.Encoding.Default.GetString(EEFileUtils.Load(Application.streamingAssetsPath + "/settings/password.txt"));
                EEPacketToServer.Send(typeof(LobbyServerSubservers), SubserverData);
            #endif
        }

        protected override void Loop()
        {
            base.Loop();
        }

        public void ReceiveAsClient(object data)
        {
            return;
            if (!EEClientGlobalEvents.IsConnected || !EEServerGlobalEvents.IsActive) return;
            print(data);
            print(GetSelf<EEHTTP>().url);
            lastDate = Time.time + 60;
            GetSelf<EEHTTP>().Call();
        }
    }
}
