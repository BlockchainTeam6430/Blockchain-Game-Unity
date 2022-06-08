using Plugins.EntenEller.Base.Scripts.Addons.XNode;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace NodeSystem
{
    public class TextureNode : EEOutputNode
    {
        [HideLabel, PreviewField(150)]
        public Texture Texture;
    }
}