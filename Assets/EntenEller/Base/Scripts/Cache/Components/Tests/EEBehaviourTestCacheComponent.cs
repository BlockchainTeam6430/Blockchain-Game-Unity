using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.UnitTests;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Tests
{
    public class EEBehaviourTestCacheComponent : EEBehaviourTest
    {
        private EEGameObject _MiddleTestGameObject;
        protected EEGameObject MiddleTestGameObject
        {
            get
            {
                if (_MiddleTestGameObject) return _MiddleTestGameObject;
                _MiddleTestGameObject = gameObject.GetComponentsInChildren<CapsuleCollider>(true).
                    First(a => a.gameObject.name == "Middle (1)").GetComponent<EEGameObject>();
                return _MiddleTestGameObject;
            }
        }
    }
}
