using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class FloatUtils
    {
        public static bool IsAlmostZero(this float a, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return Mathf.Abs(a) <= measurementAccuracy;
        }

        public static bool IsAlmostEqual(this float a, float b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return Mathf.Abs(b - a) <= measurementAccuracy;
        }

        public static bool IsInRange(this float a, float b, float c)
        {
            if (b > c) return a >= c && a <= b;
            return a >= b && a <= c;
        }
        
        public static void Switch(ref float a, ref float b)
        {
            var c = a;
            a = b;
            b = c;
        }
    }
}
