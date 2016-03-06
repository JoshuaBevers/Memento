using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

namespace SurrogateClasses
{
    public class SurrogateQuaternion : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Quaternion q = (Quaternion)obj;
            info.AddValue("w", q.w);
            info.AddValue("x", q.x);
            info.AddValue("y", q.y);
            info.AddValue("z", q.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Quaternion q = (Quaternion)obj;
            q.w = info.GetSingle("w");
            q.x = info.GetSingle("x");
            q.y = info.GetSingle("y");
            q.z = info.GetSingle("z");
            return q;
        }
    }
}
