using System;
using System.Collections.Generic;
using NodeSystem.Dialog;
using Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.Sorter;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Addons.XNode.Dialogs.DialogSystem.UI
{
    public class DialogUISpawnerPlayer : EEBehaviour
    {
        [SerializeField] private Transform nodeToStick = null;
        [SerializeField] private EEGameObject prefabUI = null;
        public Dictionary<DialogPlayerNode, EEGameObject> SpawnedPrefabs = new Dictionary<DialogPlayerNode, EEGameObject>();
        public event Action<DialogNode, EEGameObject> PrefabSpawnedEvent;
        
        protected override void EEAwake()
        {
            GetSelf<DialogNodeSorterPlayer>().ReadySuperAction.Event += () =>
            {
                DisableOldPrefabs();
                CreateNewPrefabs();
                
                void DisableOldPrefabs()
                {
                    SpawnedPrefabs.ForEach(a => a.Value.Destroy());
                    SpawnedPrefabs.Clear();
                }

                void CreateNewPrefabs()
                {
                    var nodes = GetSelf<DialogNodeSorterPlayer>().DialogNodes;
                    foreach (var node in nodes)
                    {
                        var obj = Instantiate(prefabUI, nodeToStick);
                        SpawnedPrefabs.Add((DialogPlayerNode) node, obj);
                        PrefabSpawnedEvent.Call(node, obj);
                    }
                }
            };
        }
    }
}