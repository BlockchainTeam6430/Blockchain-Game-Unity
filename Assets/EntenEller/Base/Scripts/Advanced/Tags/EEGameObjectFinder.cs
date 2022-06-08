using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Debugs;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Tags
{
    [Serializable]
    public class EEGameObjectFinder 
    {
        public GameObjectType Type;
        private bool isInitialized;
        
        [ShowIf("Type", GameObjectType.EETag)]
        [SerializeField] private string eeTag = string.Empty;
        
        [ShowIf("Type", GameObjectType.EEGameObject)]
        [SerializeField] private EEGameObject eeGameObject = null;

        [ShowIf("Type", GameObjectType.EEGameObjectList)]
        [SerializeField] private List<EEGameObject> eeGameObjects = new List<EEGameObject>();

        public EEGameObject GetSingle(MonoBehaviour self)
        {
            if (!isInitialized) Initialize(self);
            return eeGameObjects[0];
        }
        
        public List<EEGameObject> GetAll(MonoBehaviour self)
        {
            if (!isInitialized) Initialize(self);
            return eeGameObjects;
        }
        
        public EEGameObject GetByIndex(MonoBehaviour self, int index)
        {
            if (!isInitialized) Initialize(self);
            return eeGameObjects[index];
        }

        private void Initialize(MonoBehaviour self)
        {
            isInitialized = true;
            switch (Type)
            {
                case GameObjectType.Self:
                    eeGameObjects = new List<EEGameObject> {self.GetEEGameObject()};
                    break;
                case GameObjectType.Parent:
                    eeGameObjects = new List<EEGameObject> {self.transform.parent.GetEEGameObject()};
                    break;
                case GameObjectType.EEGameObject:
                    eeGameObjects = new List<EEGameObject> {eeGameObject};
                    break;
                case GameObjectType.EEGameObjectList:
                    break;
                case GameObjectType.EETag:
                    eeGameObjects = EETagUtils.FindEETagsInScenes(eeTag).Select(a => a.GetEEGameObject()).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (!eeGameObjects.Any()) EEException.Call(this, "No GameObjects found!");
        }
        
        public enum GameObjectType
        {
            Self,
            Parent,
            EEGameObject,
            EEGameObjectList,
            EETag
        }
    }
}
