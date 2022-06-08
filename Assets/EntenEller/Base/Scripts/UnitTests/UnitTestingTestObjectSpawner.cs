using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestingTestObjectSpawner : EEBehaviour
    {
        public static Action<EEBehaviourTest> CreatedUnitTestGameObjectEvent;

        public void Call()
        {
            var testScripts = Resources.LoadAll<EEBehaviourTest>(string.Empty);
            foreach (var testScript in testScripts)
            {
                CreatedUnitTestGameObjectEvent.Call(testScript);
            }
        }
    }
}
