using Plugins.EntenEller.Base.Scripts.Advanced.Cameras;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Canvases
{
    public class MainCameraCanvasSetter : EEBehaviour
    {
        protected override void EEEnable()
        {
            base.EEEnable();
            GetSelf<Canvas>().worldCamera = CameraUtils.MainCamera;
        }
    }
}
