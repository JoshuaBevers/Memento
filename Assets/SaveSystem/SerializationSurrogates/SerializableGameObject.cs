using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

namespace SurrogateClasses
{
    public class SerializableGameObject : ISerializationSurrogate
    {

        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            GameObject g = (GameObject)obj;
            //GameObject variables
            info.AddValue("layer", g.layer);
            info.AddValue("tag", g.tag);
            info.AddValue("name", g.name);
            info.AddValue("isActive", g.activeSelf);
            //Transform
            info.AddValue("localPosition", g.transform.localPosition, typeof(Vector3));
            info.AddValue("localRotation", g.transform.localRotation, typeof(Quaternion));
            info.AddValue("localScale", g.transform.localScale, typeof(Vector3));
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            GameObject g = new GameObject();
            //GameObject variables
            g.tag = info.GetString("tag");
            g.layer = info.GetInt16("layer");
            g.name = info.GetString("name");
            g.SetActive(info.GetBoolean("isActive"));
            //Transform
            g.transform.localPosition = (Vector3)info.GetValue("localPosition",typeof(Vector3));
            g.transform.localScale = (Vector3)info.GetValue("localScale", typeof(Vector3));
            g.transform.localRotation = (Quaternion)info.GetValue("localRotation", typeof(Quaternion));
            return g;
        }
    }
}

