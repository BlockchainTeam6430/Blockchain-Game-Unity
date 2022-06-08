using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Sprites
{
    public class EESprite : EEBehaviour
    {
        public static Sprite Texture2DToSprite (Texture2D texture2D)
        {
            var rect = new Rect(0, 0, texture2D.width, texture2D.height);
            var pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture2D, rect, pivot);
        }
    }
}
