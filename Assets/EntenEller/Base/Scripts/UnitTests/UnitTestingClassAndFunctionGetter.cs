using System;
using System.Reflection;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestingClassAndFunctionGetter : EEBehaviour
    {
        public static Action<EEBehaviourTest, Type, MethodInfo> GotDataEvent;
        protected override void EEAwake()
        {
            base.EEAwake();
            UnitTestingTestObjectSpawner.CreatedUnitTestGameObjectEvent += testObject =>
            {
                var type = testObject.GetType();
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    if (!method.Name.Contains("Test")) continue;
                    GotDataEvent.Call(testObject, type, method);
                }
            };
        }
    }
}