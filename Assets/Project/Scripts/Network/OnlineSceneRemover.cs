using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Network
{
    public class OnlineSceneRemover : EEBehaviour
    {
        public void Unload()
        {
            for (var i = 0; i < SceneManager.sceneCount; i++) 
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == "SinglePlayer" && scene.isLoaded) 
                {
                    SceneManager.UnloadSceneAsync("SinglePlayer");
                }
            }
        }
    }
}
