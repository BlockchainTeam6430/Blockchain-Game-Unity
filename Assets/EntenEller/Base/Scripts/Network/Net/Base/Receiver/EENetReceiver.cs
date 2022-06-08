using System;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.Patterns.Pool;
using Plugins.EntenEller.Base.Scripts.Patterns.Singleton;
using Project.Scripts.Network.Lobby;

namespace EntenEller.Base.Scripts.Network.Net.Base.Receiver
{
    public static class EENetReceiver
    {
        public static void Receive(EEPacket packet, EEPeer peer = null)
        {
            IEENetReceiverClient iClient = null;
            IEENetReceiverServer iServer = null;
            try
            {
                var type = Type.GetType(packet.ScriptName);
                if (!packet.IsForServer)
                {
                    if (EEServerGlobalEvents.IsActive)
                    {
                        if (packet.ScriptName != "Project.Scripts.Network.Lobby.LobbySubserver")
                        {
                            return;
                        }
                    }
                }
                    
                if (packet.I != -1)
                {
                    var poolObject = EEPool.GetPoolObjectByJ(EESingleton.Get<EENetPoolManager>().GetOrigin(packet.I), packet.J);
                    if (packet.IsForServer)
                    {
                        iServer = (IEENetReceiverServer) poolObject.GetComponentInChildren(type, true);
                    }
                    else
                    {
                        iClient = (IEENetReceiverClient) poolObject.GetComponentInChildren(type, true);
                    }
                }
                else
                {
                    var component = EESingleton.Get(type);
                    if (packet.IsForServer)
                    {
                        iServer = (IEENetReceiverServer) component;
                    }
                    else
                    {
                        iClient = (IEENetReceiverClient) component;
                    }
                }
            
                if (packet.IsForServer)
                {
                    iServer.ReceiveAsServer(peer, packet.Data);
                }
                else
                {
                    iClient.ReceiveAsClient(packet.Data);
                }
            }
            catch
            {
                //
            }
        }
    }
}
