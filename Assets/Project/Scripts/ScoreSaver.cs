using System.Collections.Generic;
using System.Text;
using EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Files;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Network;
using Project.Scripts.Network.Lobby;
using UnityEngine;

namespace Project.Scripts
{
    public class ScoreSaver : EEBehaviour
    {
        public static ScoreSaver Instance;

        protected override void EEAwake()
        {
            base.EEAwake();
            Instance = this;
        }
        
        public void Send(bool result, PointSystem pointSystem, SubserverData subserverData)
        {
            var dictionary = new Dictionary<string, string>
            {
                {"Wallet", subserverData.Wallet},
                {"Points", pointSystem.points.ToString()},
                {"is_pvp", LobbySubserver.SubserverData.MaxPlayers == 1 ? "0" : "1"},
                {"Wins", result ? "1" : "0"},
                {"Loses", result ? "0" : "1"},
                {"Password", Encoding.Default.GetString(EEFileUtils.Load(Application.streamingAssetsPath + "/settings/password-php.txt"))}
            };
            GetSelf<EEHTTPStringSender>().SetNewData(dictionary);
            GetSelf<EEHTTP>().Call();
        }
    }
}