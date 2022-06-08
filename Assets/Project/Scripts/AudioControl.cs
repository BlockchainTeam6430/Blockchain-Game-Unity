using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class AudioControl : EEBehaviourLoop
    {
        public static float SoundVolume;
        public static float MusicVolume;
        [SerializeField] private Slider sound, music;

        protected override void EEAwake()
        {
            base.EEAwake();
            SoundVolume = PlayerPrefs.GetFloat("sound", 1);
            MusicVolume = PlayerPrefs.GetFloat("music", 1);
            sound.value = SoundVolume;
            music.value = MusicVolume;

            sound.onValueChanged.AddListener(SoundChange);
            music.onValueChanged.AddListener(MusicChange);
            
        }

        private static void SoundChange(float a)
        {
            PlayerPrefs.SetFloat("sound", SoundVolume);
            PlayerPrefs.Save();
        }

        private static void MusicChange(float a)
        {
            PlayerPrefs.SetFloat("music", MusicVolume);
            PlayerPrefs.Save();
        }
        
        protected override void Loop()
        {
            base.Loop();
            SoundVolume = sound.value;
            MusicVolume = music.value;
        }
    }
}
