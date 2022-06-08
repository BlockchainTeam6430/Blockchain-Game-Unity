using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UnitTests
{
    public class UnitTestingAssertFailListener : EEBehaviour
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            Application.logMessageReceived += OnLogCallback;
        }
        
        protected override void EEDestroy()
        {
            base.EEDestroy();
            Application.logMessageReceived -= OnLogCallback;
        }
        
        private static void OnLogCallback(string condition, string trace, LogType type)
        {
            if (type != LogType.Exception) return;
            if (!Application.isPlaying) return;
            EEComponentUtils.FindAll<UnitTestButton>().ForEach(a => { a.CheckExceptionMessage(trace); });
        }
    }
}
