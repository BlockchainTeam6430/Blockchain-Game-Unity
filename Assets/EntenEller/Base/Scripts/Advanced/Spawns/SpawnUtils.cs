using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Spawns
{
    public static class SpawnUtils
    {
        public static T Spawn<T>(T original) where T : Component
        {
            var gameObject = original.gameObject;
            gameObject.SetActive(false);
            var obj = Object.Instantiate(original);
            gameObject.SetActive(true);
            
            var eeGameObject = obj.GetEEGameObject();
            eeGameObject.Spawn();
            EETagUtils.Spawn(eeGameObject);
            
            return obj;
        }
    }
}