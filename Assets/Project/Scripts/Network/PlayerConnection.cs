using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Libraries.Websocket;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Project.Scripts.Network.Lobby;

namespace Project.Scripts.Network
{
    public class PlayerConnection : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            EEClientGlobalEvents.ConnectedEvent += ClientConnected;
            GetSelf<WebsocketClient>().SetURL(LobbyClient.SubserverData.URL);
            GetSelf<WebsocketClient>().On();
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            EEClientGlobalEvents.ConnectedEvent -= ClientConnected;
        }

        private static void ClientConnected()
        {
            EEPacketToServer.Send(typeof(PlayersReadyManager), LobbyClient.SubserverData);
        }
    }
}
