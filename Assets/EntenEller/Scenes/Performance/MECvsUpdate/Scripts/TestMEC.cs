using System.Collections.Generic;
using MEC;
using Plugins.EntenEller.Scenes.Performance.Base;
using UnityEngine;

namespace Plugins.EntenEller.Scenes.Performance.MECvsUpdate
{
    public class TestMEC : MonoBehaviour
    {
        private void Awake()
        {
            Timing.RunCoroutine(Go());
        }

        private IEnumerator<float> Go()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                TestPerformance.Easy();
            }
        }
    }
}