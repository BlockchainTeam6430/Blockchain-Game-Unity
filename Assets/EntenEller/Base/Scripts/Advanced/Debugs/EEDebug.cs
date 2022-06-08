using System.Text;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Debugs
{
    public static class EEDebug
    {
        private static StringBuilder builder = new StringBuilder();

        public static void Log(EEDebugTag tag, object message)
        {
            #if DEBUG
             //   if (!EEDebugWindow.IsActive(tag)) return;
                if (message == null) message = "[NULL]";
                
                builder.Clear();
                builder.Append("[");
                builder.Append(tag);
                builder.Append("] ");
                builder.Append(message);
                builder.Append(", Frame=");
                builder.Append(Time.frameCount);
                builder.Append(", Time=");
                builder.Append(Time.time);
                Debug.Log(builder.ToString());
            #endif
        }
        
        public static void Log(object message)
        {
            Log(EEDebugTag.Default, message);
        }

        public static void Ray(Vector3 pos, Vector3 dir = default, Color color = default)
        {
            if (dir == default) dir = Vector3.up;
            if (color == default) color = Color.magenta;
            dir *= 100;
            Debug.DrawRay(pos, dir, color, 9999);
        }

        public static void ShowProblemObject(GameObject gameObject)
        {
            var id = gameObject.GetInstanceID();
            gameObject.name = id.ToString();
            Log(id);
        }
    }
}
