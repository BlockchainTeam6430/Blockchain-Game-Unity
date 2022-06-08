using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts
{
    public class EEAudio : EEBehaviourLoop
    {
        [SerializeField] private bool isMusic = false;

        protected override void Loop()
        {
            base.Loop();
            GetSelf<AudioSource>().volume = isMusic ? AudioControl.MusicVolume : AudioControl.SoundVolume;
        }
    }
}
