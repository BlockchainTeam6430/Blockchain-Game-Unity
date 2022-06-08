using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Parent.Tests.Resources
{
    public class TestCacheComponentParent : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetFirstParentTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetParent<Transform>();
            Assert.AreEqual(ts.name, "Parent");
        }
        
        public IEnumerator<float> TestGetAllParentTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllParent<Transform>();
            Assert.AreEqual(array.Length, 2);
        }
        
        public IEnumerator<float> TestGetSphereCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetParent<SphereCollider>();
            Assert.AreNotEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetCapsuleCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetParent<CapsuleCollider>();
            Assert.AreEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetBoxCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetParent<BoxCollider>();
            Assert.AreEqual(coll, null);
        }
    }
}