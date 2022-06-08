using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class HealthSystem : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private EESuperAction deathMaster;
        [SerializeField] private EEGameObjectFinder text, textEnemy;
        public int health;
        
        private void Awake()
        {
            text.GetSingle(this).GetSelf<EETextAnimationNumberLerp>().Set(health);
        }

        public void Remove(int amount)
        {
            health -= amount;
            EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(HealthSystem), health, GetSelf<EENetPoolObject>()));
            if (health > 0) return;
            if (health <= 0) deathMaster.Call();
        }

        public void ReceiveAsClient(object data)
        {
            health = (int) data;
            if (GetSelf<PlayerOwner>().IsMine) text.GetSingle(this).GetSelf<EEText>().SetData(health.ToString());
            else textEnemy.GetSingle(this).GetSelf<EEText>().SetData(health.ToString());
        }
    }
}
