using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Keyboard
{
    public class EEUnityEventOnKey : EEUnityEventBinary
    {
        [SerializeField] private KeyCode keyCode;

        private void LateUpdate()
        {
            if (Input.GetKeyDown(keyCode)) EventOn();
            if (Input.GetKeyUp(keyCode)) EventOff();
        }
    }
}
