using System.Collections.Generic;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class AngleUtils
    {
        public static float ConvertTo180Style(float a)
        {
            return a > 180 ? a - 360 : a;
        }

        public static float LimitAngle(float a, float min = -90f, float max = 90f)
        {
            a = ConvertTo180Style(a);
            return Mathf.Clamp(a, min, max);
        }
        
        public static Quaternion ListToQuaternion(List<float> list)
        {
            var x = list[0];
            var y = list[1];
            var z = list[2];
            var w = list[3];
            return new Quaternion(x, y, z, w);
        }
        
        public static List<float> QuaternionToList(Quaternion q)
        {
            return new List<float>{q.x, q.y, q.z, q.w};
        }
    }
}
