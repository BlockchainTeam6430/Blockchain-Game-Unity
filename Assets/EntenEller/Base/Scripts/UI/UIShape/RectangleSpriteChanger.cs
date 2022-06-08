using Plugins.EntenEller.Base.Scripts.Advanced.Sprites;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using ThisOtherThing.UI.Shapes;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.UIShape
{
    public class RectangleSpriteChanger : EEBehaviour
    {
        public void Change(Texture2D texture2D)
        {
             GetSelf<Rectangle>().Sprite = EESprite.Texture2DToSprite(texture2D);
             GetSelf<Rectangle>().ForceMeshUpdate();
        }
    }
}
