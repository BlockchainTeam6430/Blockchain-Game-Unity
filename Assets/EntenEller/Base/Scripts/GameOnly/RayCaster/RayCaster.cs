using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.GameOnly.RayCaster
{
    public abstract class Raycaster : EEBehaviourLoop
    {
        protected List<RaycastHit> CurrentCollisions { get; set; } = new List<RaycastHit>();

        protected override void Loop()
        {
            base.Loop();
            CurrentCollisions.Clear();
            FindAllHits();
        }

        protected abstract void FindAllHits();
    }
}