using System;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.Tags;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Components
{
    [Serializable]
    public class FloatingComponent
    {
        public FloatingComponentType Type;
        [ShowIf("Type", FloatingComponentType.Defined)] public GameObject gameObject;
        [ShowIf("Type", FloatingComponentType.EETag)] public string eeTag;

        private EEGameObject _eeGameObject;
        private EEGameObject eeGameObject
        {
            get
            {
                if (_eeGameObject.IsNotNull()) return _eeGameObject;
                _eeGameObject = gameObject.GetEEGameObject();
                return _eeGameObject;
            }
            set => _eeGameObject = value;
        }

        public T Get<T>(EEBehaviour behaviour) where T : Component
        {
            if (Type == FloatingComponentType.Defined) return eeGameObject.GetSelf<T>();
            Component component;
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (Type)
            {
                case FloatingComponentType.EETag:
                    component = EETagUtils.FindEETagInScenes(eeTag).GetSelf<T>();
                    break;
                case FloatingComponentType.Self:
                    component = behaviour.GetSelf<T>();
                    break;
                case FloatingComponentType.Child:
                    component = behaviour.GetChild<T>();
                    break;
                case FloatingComponentType.Parent:
                    component = behaviour.GetParent<T>();
                    break;
                case FloatingComponentType.Neighbor:
                    component = behaviour.GetNeighbor<T>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            eeGameObject = component.GetEEGameObject();
            Type = FloatingComponentType.Defined;
#if UNITY_EDITOR
            gameObject = eeGameObject.gameObject;
#endif
            return eeGameObject.GetSelf<T>();
        }
        
        public enum FloatingComponentType
        {
            Defined,
            EETag,
            Self,
            Child,
            Parent,
            Neighbor
        }
    }
}