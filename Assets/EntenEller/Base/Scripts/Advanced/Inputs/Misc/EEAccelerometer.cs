using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Misc
{
    public class EEAccelerometer : EEBehaviourLoop
    {
        public Vector3 Axis { get; private set; }

        protected override void Loop()
        {
            base.Loop();
            Axis = Input.acceleration;
        }
    }
}
