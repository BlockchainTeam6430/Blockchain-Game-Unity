using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Project.Scripts
{
    public class ScreenAspectRatio : EEBehaviourLoop
    {
        [SerializeField] private float width = 10;   

        protected override void Loop()
        {
            base.Loop();
            var cameraCurrent = GetSelf<Camera>();
            var aspectRatio = (float) Screen.height / Screen.width;
            cameraCurrent.orthographicSize = width * aspectRatio;
        }
    }
}
