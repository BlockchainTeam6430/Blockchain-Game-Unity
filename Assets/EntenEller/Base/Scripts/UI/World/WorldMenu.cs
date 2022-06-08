using System.Collections;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.World
{
    public class WorldMenu : EEBehaviour
    {
        public Camera MainCamera { get; private set; }

        protected override void EEAwake()
        {
            StartCoroutine(WaitCamera());
        }

        private IEnumerator WaitCamera()
        {
            while (Camera.current.IsNull())
            {
                yield return null;
            }
            MainCamera = Camera.current;
        }
    }
}
