using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

namespace SurrogateClasses
{
    sealed class SurrogateVector2 : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector2 v = (Vector2)obj;
            info.AddValue("x", v.x);
            info.AddValue("y", v.y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector2 v = (Vector2)obj;
            v.x = info.GetSingle("x");
            v.y = info.GetSingle("y");
            return v;
        }
    }
}

