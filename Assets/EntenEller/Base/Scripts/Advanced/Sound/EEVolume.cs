using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Sound
{
    public class EEVolume : EEBehaviourLoop
    {
        public FloatApproach VolumeApproach;
        
        public void On()
        {
            VolumeApproach.SetTarget(1);
        }

        public void Off()
        {
            VolumeApproach.SetTarget(0);
        }

        protected override void Loop()
        {
            base.Loop();
            VolumeApproach.Proceed();
            GetSelf<AudioSource>().volume = VolumeApproach.Current;
        }
    }
}
