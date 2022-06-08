using System;
using System.Collections;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Sirenix.Utilities;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Market
{
    public class ScreenshotMaker : EEBehaviourLoop
    {
        public void Call()
        {
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/../screenshots/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
            print("Screenshot MADE!");
        }
    }
}
