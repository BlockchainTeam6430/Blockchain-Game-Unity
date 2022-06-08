using System;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.GameOnly.RayCaster
{
    public class RayCasterTarget : MonoBehaviour
    {
        public event Action InteractedEvent;

        public void Interact()
        {
            InteractedEvent.Call();
        }
    }
}