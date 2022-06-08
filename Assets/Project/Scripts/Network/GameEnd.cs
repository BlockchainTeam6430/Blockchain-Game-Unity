using Newtonsoft.Json;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Plugins.EntenEller.Base.Scripts.UI.Menu;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class GameEnd : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private EEMenuData lose, win;
        public static bool IsOver;
        
        public async void Dead()
        {
            IsOver = true;
            GetSelf<PlayerOwner>().IsDead = true;
            foreach (var peer in EEServerGlobalEvents.Clients)
            {
                EEPacketToClient.Send(peer, typeof(GameEnd), peer == GetSelf<PlayerOwner>().Peer,
                    GetSelf<EENetPoolObject>());
            }
            foreach (var playerOwner in FindObjectsOfType<PlayerOwner>())
            {
                EESingleton.Get<ScoreSaverOld>().Send(false);
                ScoreSaver.Instance.Send(playerOwner.IsDead, GetSelf<PointSystem>(), playerOwner.SubserverData);
            }

            EETagUtils.FindEETagsInScenes("gameend").ForEach(a => a.GetEEGameObject().Off());

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://game-server-3.herokuapp.com");
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("room", "0x02fc14d01F4E073829276cc2f4f94Fb4EDe1e0c4"),
                new KeyValuePair<string, string>("winner", "0x02fc14d01F4E073829276cc2f4f94Fb4EDe1e0c4")
                });
                var result = await client.PostAsync("/api/gameOver", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Debug.Log(resultContent);
            }
        }

        protected override void EEEnable()
        {
            base.EEEnable();
            IsOver = false;
        }

        public void ReceiveAsClient(object data)
        {
            IsOver = true;
            var result = (bool) data;
            gameObject.SetActive(false);
            if (result) lose.On();
            else win.On();
            EETagUtils.FindEETagsInScenes("gameend").ForEach(a => a.GetSelf<EEGameObject>().SetState(false));
            EESingleton.Get<WebsocketClient>().Off();
        }
    }
}
