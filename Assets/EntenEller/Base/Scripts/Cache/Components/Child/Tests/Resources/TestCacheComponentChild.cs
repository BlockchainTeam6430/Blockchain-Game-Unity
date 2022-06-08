using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Child.Tests.Resources
{
    public class TestCacheComponentChild : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetFirstChildTransform()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetChild<Transform>();
            Assert.AreEqual(ts.name, "Child");
        }

        public IEnumerator<float> TestGetAllChildTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllChild<Transform>();
            Assert.AreEqual(array.Length, 4);
        }

        public IEnumerator<float> TestGetSphereCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetChild<SphereCollider>();
            Assert.AreEqual(coll, null);
        }

        public IEnumerator<float> TestGetCapsuleCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetChild<CapsuleCollider>();
            Assert.AreEqual(coll, null);
        }

        public IEnumerator<float> TestGetBoxCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetChild<BoxCollider>();
            Assert.AreNotEqual(coll, null);
        }
    }
}