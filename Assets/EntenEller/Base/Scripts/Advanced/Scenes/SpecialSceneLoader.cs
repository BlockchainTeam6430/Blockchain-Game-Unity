using System;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Scenes
{
    public class SpecialSceneLoader : MonoBehaviour
    {
        private void Awake()
        {
            EESceneData.SceneLoadingFinished();
        }
    }
}
