using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Tests;
using UnityEngine;
using UnityEngine.Assertions;

namespace Plugins.EntenEller.Base.Scripts.Cache.Components.Neighbor.Tests.Resources
{
    public class TestCacheComponentNeighbor : EEBehaviourTestCacheComponent
    {
        public IEnumerator<float> TestGetMeshCollider()
        {
            yield return Timing.WaitForOneFrame;
            var ts = MiddleTestGameObject.GetNeighbor<MeshCollider>();
            Assert.AreEqual(ts.name, "Middle");
        }
        
        public IEnumerator<float> TestGetAllNeighborTransforms()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllNeighbor<Transform>();
            Assert.AreEqual(array.Length, 2);
        }
        
        public IEnumerator<float> TestGetSphereCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetNeighbor<SphereCollider>();
            Assert.AreEqual(coll, null);
        }
        
        public IEnumerator<float> TestGetAllCapsuleColliders()
        {
            yield return Timing.WaitForOneFrame;
            var array = MiddleTestGameObject.GetAllNeighbor<CapsuleCollider>();
            Assert.AreEqual(array.Length, 6);
        }
        
        public IEnumerator<float> TestGetBoxCollider()
        {
            yield return Timing.WaitForOneFrame;
            var coll = MiddleTestGameObject.GetNeighbor<BoxCollider>();
            Assert.AreEqual(coll, null);
        }
    }
}