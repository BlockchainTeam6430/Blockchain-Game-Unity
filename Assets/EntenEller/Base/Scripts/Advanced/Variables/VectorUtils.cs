using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class VectorUtils
    {
        public static bool IsAlmostEqual(this Vector3 a, Vector3 b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return (a - b).magnitude.IsAlmostZero(measurementAccuracy);
        }
        
        public static bool IsAlmostEqual(this Vector2 a, Vector2 b, float measurementAccuracy = EEConstants.MeasurementAccuracyHigh)
        {
            return (a - b).magnitude.IsAlmostZero(measurementAccuracy);
        }

        public static Vector3 ListToV3(List<float> list)
        {
            var x = list[0];
            var y = list[1];
            var z = list[2];
            return new Vector3(x, y, z);
        }
        
        public static List<float> V3ToList(Vector3 v3)
        {
            return new List<float>{v3.x, v3.y, v3.z};
        }
        
        public static Vector3 RandomV3(Vector3 a, Vector3 b)
        {
            return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
        }
    }
}