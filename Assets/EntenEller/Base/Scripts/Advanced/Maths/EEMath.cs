using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Maths
{
    public static class EEMath 
    {
        public static float GetXOnLine2D(Vector2 pos0, Vector2 pos1, float y)
        {
            return pos0.x + (pos1.x - pos0.x) * ((y - pos0.y) / (pos1.y - pos0.y));
        }
    }
}