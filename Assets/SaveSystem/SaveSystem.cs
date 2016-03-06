using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.IO;

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

    public static bool SaveMemento(String filePath, Memento m)
    {
        try
        {
            BinaryFormatter formatter = getFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, m);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error serializing Memento\n" + e.Message + e.StackTrace);
            return false;
        }
    }
    public static Memento LoadMemento(String filePath)
    {
        try
        {
            BinaryFormatter formatter = getFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Memento m = (Memento)formatter.Deserialize(stream);
                return m;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading Memento\n" + e.Message + e.StackTrace);
            return null;
        }
    }

    /// <summary>
    /// Returns a BinaryFormatter with a custom SurrogateSelector for Unity Types.
    /// </summary>
    /// <returns></returns>
    public static BinaryFormatter getFormatter()
    {
        BinaryFormatter f = new BinaryFormatter();
        f.SurrogateSelector = selector;
        return f;
    }

    private static SurrogateSelector _selector;



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
