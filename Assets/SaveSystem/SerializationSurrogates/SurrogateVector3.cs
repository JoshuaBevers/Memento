using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

namespace SurrogateClasses
{
    sealed class SurrogateVector3 : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v = (Vector3)obj;
            info.AddValue("x", v.x);
            info.AddValue("y", v.y);
            info.AddValue("z", v.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v = (Vector3)obj;
            v.x = info.GetSingle("x");
            v.y = info.GetSingle("y");
            v.z = info.GetSingle("z");
            return v;
        }
    }
}

