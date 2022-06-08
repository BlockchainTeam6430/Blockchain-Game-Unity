using System;
using System.Globalization;
using Plugins.EntenEller.Base.Scripts.Advanced.Variables.Approach;
using Plugins.EntenEller.Base.Scripts.Translation;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.UI.Text
{
    public class EETextAnimationNumberLerp : EETextSimple
    {
        [SerializeField] private FloatApproach Approach;
        [SerializeField] private int FractionalPart = 0;
        
        protected override void EEAwake()
        {
            base.EEAwake();
            Approach.SetCurrent(int.Parse(Data));
        }

        public void Set(int value)
        {
            Approach.SetTarget(value);
        }

        protected override void MenuLoop()
        {
            base.MenuLoop();
            if (!Approach.IsReached)
            {
                SetData(Math.Round(Approach.Current, FractionalPart).ToString(CultureInfo.InvariantCulture));
            }
            Approach.Proceed();
        }
    }
}
