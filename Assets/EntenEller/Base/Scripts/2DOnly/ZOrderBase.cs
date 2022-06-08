using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts._2DOnly
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public abstract class ZOrderBase : EEBehaviourLoop
    {
        protected override void Loop()
        {
            base.Loop();
            SetZ();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying) SetZ();
        }
#endif
        protected abstract void SetZ ();
    }
}
