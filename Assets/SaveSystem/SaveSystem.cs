using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public static class SaveSystem
{
    [Serializable]
    public class Memento {
        public object id = null;
        public Dictionary<object,object> vals = new Dictionary<object,object>();
        public override string ToString()
        {
            String s = "Id: " + "\n";
            s += id;
            s += " -----------------\n ";
            foreach (KeyValuePair<object,object> e in vals)
            {
                s += e.Key + " | " + e.Value + '\n';
            }
            return s;
        }

    }
    public interface HasMemento {
        Memento getMemento();
        void LoadMemento(Memento s);
    }




    private static SurrogateSelector _selector;
    public static BinaryFormatter getFormatter()
    {
        BinaryFormatter f = new BinaryFormatter();
        f.SurrogateSelector = selector;
        return f;
    }



    private static SurrogateSelector selector
    {
        get
        {
            if (_selector != null)
                return _selector;
            else
            {
                SetSurrogate();
                return _selector;
            }
        }
    }
    private static void SetSurrogate()
    {
        _selector = new SurrogateSelector();

        //Quaternion
        _selector.AddSurrogate(
            typeof(Quaternion),
            new StreamingContext(StreamingContextStates.All),
            new SurrogateClasses.SurrogateQuaternion());

        //Vector2
        _selector.AddSurrogate(
            typeof(Vector2),
            new StreamingContext(StreamingContextStates.All),
            new SurrogateClasses.SurrogateVector2());

        //Vector3
        _selector.AddSurrogate(
            typeof(Vector3),
            new StreamingContext(StreamingContextStates.All),
            new SurrogateClasses.SurrogateVector3());

        //GameObject
        _selector.AddSurrogate(typeof(GameObject),
            new StreamingContext(StreamingContextStates.All),
            new SurrogateClasses.SerializableGameObject());
    }
}
