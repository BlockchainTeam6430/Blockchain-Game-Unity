using Plugins.EntenEller.Base.Scripts.Addons.XNode;
using Plugins.EntenEller.Base.Scripts.GameOnly.Character.Description;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace NodeSystem.Dialog
{
    public class CharacterDescriptionNode : EEOutputNode
    {
        public CharacterDescription CharacterDescription;

        [HideLabel, PreviewField(175), ShowInInspector]
        public Texture2D Portrait => CharacterDescription != null ? CharacterDescription.Texture2D : Texture2D.linearGrayTexture;
        [ShowInInspector] public string Name => CharacterDescription != null ? CharacterDescription.Name : "Unknown";
    }
}