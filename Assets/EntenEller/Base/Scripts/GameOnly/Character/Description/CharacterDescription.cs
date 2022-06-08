using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.GameOnly.Character.Description
{
    public class CharacterDescription : EEBehaviour
    {
        [HideLabel, PreviewField(150)]
        public Texture2D Texture2D;
        public string Name;
        [TextArea(10, 10)]
        public string Description;
    }
}
