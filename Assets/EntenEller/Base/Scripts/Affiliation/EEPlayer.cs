using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;

namespace Plugins.EntenEller.Base.Scripts.Affiliation
{
    public class EEPlayer : EEBehaviour, IEENetReceiverClient
    {
        public string TagPlayer;
        public string TagTeam;
        public bool IsMine;
        public EESuperAction ChangedTeamSuperAction;

        protected override void EEAwake()
        {
            base.EEAwake();
            if (GetSelf<EESpawner>()) GetSelf<EESpawner>().BeforeEnable += BeforeEnable;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            if (GetSelf<EESpawner>()) GetSelf<EESpawner>().BeforeEnable -= BeforeEnable;
        }

        private void BeforeEnable(EEGameObject obj)
        {
            obj.GetSelf<EEPlayer>().TagPlayer = TagPlayer;
            obj.GetSelf<EEPlayer>().TagTeam = TagTeam;
        }

        public void SetTagPlayer(string tagPlayer)
        {
            TagPlayer = tagPlayer;
        }
        
        public bool IsSamePlayer(EEPlayer eePlayer)
        {
            return TagPlayer == eePlayer.TagPlayer;
        }
        
        public bool IsSamePlayer(string tagPlayer)
        {
            return TagPlayer == tagPlayer;
        }
        
        public void SetTagTeam(string tagTeam)
        {
            if (TagTeam == tagTeam) return;
            TagTeam = tagTeam;
            ChangedTeamSuperAction.Call(ComponentID);
        }
        
        public bool IsSameTeam(EEPlayer eePlayer)
        {
            return TagTeam == eePlayer.TagTeam;
        }
        
        public bool IsSameTeam(string tagTeam)
        {
            return TagTeam == tagTeam;
        }

        public void ReceiveAsClient(object data)
        {
            IsMine = true;
        }
    }
}
