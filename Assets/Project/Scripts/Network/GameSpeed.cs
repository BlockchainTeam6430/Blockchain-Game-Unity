using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class GameSpeed : EEBehaviourLoop, IEENetReceiverClient
    {
         [SerializeField] private float acceleration;
         
         protected override void Loop()
         {
             base.Loop();
             
             if (GameEnd.IsOver)
             {
                 Time.timeScale = 0;
                 EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(GameSpeed), 0));
                 this.GetEEGameObject().SetState(false);
                 return;
             }

             if (EEServerGlobalEvents.IsActive)
             {
                 Time.timeScale += acceleration * Time.unscaledDeltaTime;
                 EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(GameSpeed), Time.timeScale));
             }
         }
         
         public void ReceiveAsClient(object data)
         {
             Time.timeScale = (float) data;
         }
    }
}