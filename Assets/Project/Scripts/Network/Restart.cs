using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Plugins.EntenEller.Base.Scripts.Timers;
using Project.Scripts.Network.Lobby;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class Restart : EEBehaviourLoop
    {
        private bool isPointsSend;
        private bool isMoneySend = true;

        protected override void EEAwake()
        {
            base.EEAwake();
            EEServerGlobalEvents.ClientDisconnectedEvent += Disconnect;
        }

        private void Disconnect (EEPeer peer)
        {
            if (GameEnd.IsOver) return;
            var target = FindObjectsOfType<PlayerOwner>(true).FirstOrDefault(a => a.Peer == peer);
            if (target == null) return;
            EEServerGlobalEvents.ClientDisconnectedEvent -= Disconnect;
            print("BAD DISCONNECT!");
            target.GetSelf<GameEnd>().Dead();
        }
        
        public void PointsSent()
        {
            isPointsSend = true;
        }
        
        protected override void Loop()
        {
            base.Loop();
            if (!isPointsSend || !isMoneySend) return;
            enabled = false;
            EESingleton.Get<WebsocketClient>().Off();
            #if !UNITY_WEBGL
            EESingleton.Get<WebsocketServer>().Off();
#endif
        }

        public static void Do()
        {
            Application.Quit();
        }
    }
}