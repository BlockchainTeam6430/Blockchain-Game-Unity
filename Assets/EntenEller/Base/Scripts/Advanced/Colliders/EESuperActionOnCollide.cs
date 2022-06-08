using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Colliders
{
    public class EESuperActionOnCollide : EEUnityEvent
    {
        private void OnCollisionEnter(Collision other)
        {
            Call();
        }
    }
}