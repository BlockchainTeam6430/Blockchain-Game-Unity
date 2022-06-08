using System;
using System.Collections.Generic;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Scenes
{
    [CreateAssetMenu(fileName = "EEMenuData", menuName = "EntenEller/Scene/EESceneData", order = 1)]
    public class EESceneData : ScriptableObject
    {
        public string Name;
        public bool IsMain;
        
        public static event Action ScenesStartedChangesEvent;
        public static event Action ScenesRawFinishedChangesEvent;
        
        private static bool isInitialized;
        public static readonly List<Scene> scenesToUnload = new List<Scene>();
        private static readonly List<string> scenesToLoad = new List<string>();
        private static string mainScene;
        public static bool IsLoading;
        private static int frame;
        
        public void Load(EESceneData sceneData)
        {
            if (frame != Time.frameCount) ScenesStartedChangesEvent.Call();
            frame = Time.frameCount;
            
            IsLoading = true;
            if (!isInitialized)
            {
                isInitialized = true;
                SceneManager.sceneUnloaded += scene =>
                {
                    scenesToUnload.Remove(scene);
                    if (scenesToUnload.Count != 0) return;
                    EEDebug.Log(EEDebugTag.Scene, "Unloaded: " + scene.name);
                    scenesToLoad.ForEach(a => SceneManager.LoadScene(a, LoadSceneMode.Additive));
                };
                SceneManager.sceneLoaded += (scene, mode) =>
                {
                    scenesToLoad.RemoveAll(a => a == scene.name);
                    EEDebug.Log(EEDebugTag.Scene, "Loaded: " + scene.name);
                    EEDebug.Log(EEDebugTag.Scene, "Left: " + scenesToLoad.Count);
                    if (scene.name == mainScene)
                    {
                        EEDebug.Log(EEDebugTag.Scene, "Set main scene: " + scene.name);
                        SceneManager.SetActiveScene(scene);
                    }
                    if (scenesToLoad.Count != 0) return;
                    SceneLoadingFinished();
                };
            }

            if (sceneData.IsMain)
            {
                mainScene = sceneData.Name;
                EEDebug.Log(EEDebugTag.Scene, "Trying to set MainScene: " + mainScene);
            }
            
            if (scenesToUnload.Count == 0)
            {
                SceneManager.LoadScene(sceneData.Name, LoadSceneMode.Additive);
            }
            else
            {
                scenesToLoad.Add(sceneData.Name);
            }
        }

        public static void SceneLoadingFinished()
        {
            EEDebug.Log(EEDebugTag.Scene, "Finished loading scenes");
            EETime.StartTimer(new EETime.EETimerData
            {
                Action = () =>
                {
                    IsLoading = false;
                    ScenesRawFinishedChangesEvent.Call();
                    var list = FindObjectsOfType<EEBehaviourInit>(true);
                    for (var i = 0; i < list.Length; i++)
                    {
                        var init = list[i];
                        init.SceneAwake();
                        init.SceneEnable();
                    }
                },
                FinalTime = 0.1f
            });
        }

        public void Unload(EESceneData sceneData)
        {
            scenesToUnload.Add(SceneManager.GetSceneByName(sceneData.Name));
            SceneManager.UnloadSceneAsync(sceneData.Name);
        }
    }
}