using Plugins.EntenEller.Base.Scripts.Advanced.Textures;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Network.HTTP
{
    public class EEHTTPTexture : EEBehaviour
    {
        public Texture2D Texture;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            GetSelf<EEHTTPByteReceiver>().SuccessEvent += Success;
        }

        protected override void EEDestroy()
        {
            base.EEDestroy();
            GetSelf<EEHTTPByteReceiver>().SuccessEvent -= Success;
        }

        private void Success(byte[] data)
        {
            Texture = new Texture2D(1, 1, TextureFormat.RGB24, false);
            Texture.LoadImage(data);
            GetSelf<TextureChanger>().Change(Texture);
        }
    }
}
