using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Behaviours
{
    public abstract class EEBehaviourLoop : EEBehaviour
    {
        [SerializeField] protected Segment LoopType = Segment.Update;

        protected override void EEEnable()
        {
            base.EEEnable();
            Timing.RunCoroutine(LoopRoutine(), LoopType,  ComponentID);
        }

        protected override void EEDisable()
        {
            base.EEDisable();
            Timing.KillCoroutines(ComponentID);
        }

        private IEnumerator<float> LoopRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                Loop();
            }
        }
       
        protected virtual void Loop() {}
    }
}