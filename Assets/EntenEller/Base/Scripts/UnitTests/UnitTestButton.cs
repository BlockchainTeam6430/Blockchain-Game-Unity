using System;
using System.Collections.Generic;
using System.Reflection;
using Plugins.EntenEller.Base.Scripts.Advanced.GameObjects;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.UI.Text;
using Plugins.EntenEller.Base.Scripts.UI.UIShape;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using Plugins.EntenEller.Base.Scripts.Advanced.Spawns;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestButton : EESpawnerEEGameObject
    {
        public static Action<UnitTestButton> CreatedEvent;
        
        private EEBehaviourTest UnitTest;
        private Type Type;
        private MethodInfo Method;
        
        public bool IsFail;
        
        public static Color ColorFail = new Color(1f, 0.33f, 0.17f);
        public static Color ColorSuccess = new Color(0.27f, 0.58f, 0.15f);
        public static Color ColorInProgress = new Color(0.8f, 0.8f, 0.15f);
        
        public EESuperAction TestCompleted;
        private IEnumerator<float> coroutine;
        private EEBehaviourTest unitTestSpawned;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            CreatedEvent.Call(this);
            GetSelf<Button>().onClick.AddListener(() =>
            {
                if (coroutine != null) return;
                
                DespawnAll();
                
                Prefab = UnitTest.GetEEGameObject();
                unitTestSpawned = Spawn().GetSelf<EEBehaviourTest>();

                IsFail = false;
                coroutine = (IEnumerator<float>) Method.Invoke(unitTestSpawned, null);
                
                Timing.RunCoroutine(CheckIfEnded(Timing.RunCoroutine(coroutine)));

                IEnumerator<float> CheckIfEnded(CoroutineHandle runCoroutine)
                {
                    GetChild<EEShape>().FillColor = ColorInProgress;
                    while (true)
                    {
                        yield return Timing.WaitForOneFrame;
                        if (!runCoroutine.IsRunning) break;
                    }
                    GetChild<EEShape>().FillColor = IsFail ? ColorFail : ColorSuccess;
                    if (!IsFail) DespawnAll();
                    TestCompleted.Call(ComponentID);
                    coroutine = null;
                }
            });
        }

        public void Setup(EEBehaviourTest unitTest, Type type, MethodInfo method)
        {
            UnitTest = unitTest;
            Type = type;
            Method = method;
            GetChild<EETextSimple>().SetData(type.Name + " " + method.Name);
        }

        public void CheckExceptionMessage(string message)
        {
            if (message.Contains(Type.Name) && message.Contains(Method.Name)) IsFail = true;
        }
    }
}
