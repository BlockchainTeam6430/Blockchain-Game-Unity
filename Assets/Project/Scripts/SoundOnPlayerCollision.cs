using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.Triggers;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using UnityEngine;

namespace Project.Scripts
{
    public class SoundOnPlayerCollision : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private AudioSource audioToPlay;
        [SerializeField] private EETrigger eeTrigger;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            eeTrigger.EnterRightEvent += Enter;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            eeTrigger.EnterRightEvent -= Enter;
        }

        private void Enter(EETrigger trigger)
        {
            if (trigger.GetSelf<EETagHolder>().IsHavingTag("player"))
            {
                audioToPlay.transform.SetParent(null);
                EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(SoundOnPlayerCollision), "", GetSelf<EENetPoolObject>()));
            }
        }
        

        public void ReceiveAsClient(object data)
        {
            audioToPlay.transform.SetParent(null);
            audioToPlay.Play();
        }
    }
}
