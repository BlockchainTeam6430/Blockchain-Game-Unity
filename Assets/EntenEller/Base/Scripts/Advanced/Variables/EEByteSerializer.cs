using Sirenix.Serialization;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Variables
{
    public static class EEByteSerializer
    {
        public static byte[] Serialize<T>(T data)
        {
            return SerializationUtility.SerializeValue(data, DataFormat.Binary);
        }
 
        public static T Deserialize<T>(byte[] bytes)
        {
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
        }
    }
}
