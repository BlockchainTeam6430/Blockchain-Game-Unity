using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Advanced.Triggers;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Collections;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Events;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Pool;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Sender.Data;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using UnityEngine;

namespace Project.Scripts.Network
{
    public class PointSystem : EEBehaviour, IEENetReceiverClient
    {
        [SerializeField] private EEGameObjectFinder text, textEnemy;
        public int points;

        protected override void EEEnable()
        {
            base.EEEnable();
            EETagUtils.FindEETagInChildren(this, "bonus-collector").GetSelf<EETrigger2D>().EnterRightEvent += Enter;
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            EETagUtils.FindEETagInChildren(this, "bonus-collector").GetSelf<EETrigger2D>().EnterRightEvent -= Enter;
        }

        private void Enter(EETrigger eeTrigger)
        {
            var p = eeTrigger.GetSelf<CollectionInt>().List.First();
            var spawner = EETagUtils.FindEETagInChildren(this, "bonus-collector").transform.GetChild(p > 0 ? 0 : 1).GetComponent<EESpawner>();
            var obj = spawner.Spawn();
            obj.GetChild<EEText>().SetData(p.ToString());
            Add(p);
        }

        public void Add(int amount)
        {
            points += amount;
            EEServerGlobalEvents.Clients.ForEach(a => EEPacketToClient.Send(a, typeof(PointSystem), points, GetSelf<EENetPoolObject>()));
        }
        
        public void ReceiveAsClient(object data)
        {
            points = (int) data;
            if (GetSelf<PlayerOwner>().IsMine) text.GetAll(this).ForEach(a => a.GetSelf<EEText>().SetData(points.ToString()));
            else textEnemy.GetSingle(this).GetSelf<EEText>().SetData(points.ToString());
        }
    }
}
