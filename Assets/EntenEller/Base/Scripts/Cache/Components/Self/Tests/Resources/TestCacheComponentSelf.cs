using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Self.Tests.Resources
{
    public class TestCacheComponentSelf : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetSelfTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetSelf<Transform>();
            Assert.AreEqual(ts.name, "Middle (1)");
        }

        public IEnumerator<float> TestGetAllSelfTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllSelf<Transform>();
            Assert.AreEqual(array.Length, 1);
        }
        
        public IEnumerator<float> TestGetAllCapsuleColliders()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllSelf<CapsuleCollider>();
            Assert.AreEqual(array.Length, 3);
        }
        
        public IEnumerator<float> TestGetSphereCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetSelf<SphereCollider>();
            Assert.AreEqual(coll, null);
        }

        public IEnumerator<float> TestGetCapsuleCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetSelf<CapsuleCollider>();
            Assert.AreNotEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetBoxCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetSelf<BoxCollider>();
            Assert.AreEqual(coll, null);
        }
    }
}