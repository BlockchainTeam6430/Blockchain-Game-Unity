using Plugins.EntenEller.Base.Scripts.Advanced.Textures;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Plugins.EntenEller.Base.Scripts.Network.HTTP;
using Plugins.EntenEller.Base.Scripts.Network.Net.Base.Receiver;
using Project.Scripts.UI;

namespace Project.Scripts.Character
{
    public class CharacterIcon : EEBehaviour, IEENetReceiverClient
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            if (NFTImageToggle.CurrentTexture) GetSelf<TextureChanger>().Change(NFTImageToggle.CurrentTexture);
        }

        public void ReceiveAsClient(object data)
        {
            if ((string) data == "https://api.hypehippo.io/images/") return;
            GetSelf<EEHTTP>().url = (string) data + ".png";
            GetSelf<EEHTTP>().Call();
        }
    }
}
