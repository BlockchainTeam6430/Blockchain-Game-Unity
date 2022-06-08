using Plugins.EntenEller.Base.Scripts.Advanced.Sprites;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Textures
{
    public class TextureChanger : EEBehaviour
    {
        public void Change(Texture texture)
        {
            GetSelf<Renderer>().material.mainTexture = texture;
        }
        
        public void Change(Texture2D texture)
        {
            if (GetSelf<Image>())
            {
                GetSelf<Image>().sprite = EESprite.Texture2DToSprite(texture);
                return;
            }
            GetSelf<Renderer>().material.mainTexture = texture;
        }
    }
}
