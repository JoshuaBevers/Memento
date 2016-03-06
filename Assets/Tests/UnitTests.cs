using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;
using System.Linq;

public class UnitTests : MonoBehaviour {

    private class TestMethod : System.Attribute { };

	// Use this for initialization
	void Start () {
        Debug.Log(this.GetType());
        MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        Debug.Log(methods.Length);
        foreach (MethodInfo m in methods)
        {
            if (m.GetCustomAttributes(typeof(TestMethod),false).Length > 0)
            {
                Debug.Log(m.Name);
                m.Invoke(this, null);
            }
        }
	}

    [TestMethod]
    void TestVector3()
    {
        Vector3 v = new Vector3(1, 2, 3);
        Vector3 v2 = SerializeObject<Vector3>(v);
        Assert.AreEqual(v, v2);
        //Debug.Log(v + " | " + v2);
    }
    [TestMethod]
    void TestQuaternion()
    {
        Quaternion q = new Quaternion(1, 2, 3, 4);
        Quaternion q2 = SerializeObject<Quaternion>(q);
        Assert.AreEqual(q, q2);
        //Debug.Log(q + " | " + q2);
    }
    [TestMethod]
    void TestVector2()
    {
        Vector2 v = new Vector2(1, 2);
        Vector2 v2 = SerializeObject<Vector2>(v);
        Assert.AreEqual(v, v2);
        //Debug.Log(v + " | " + v2);
    }
    [TestMethod]
    void gameObject()
    {
        GameObject g = new GameObject("Test");
        g.transform.localPosition = new Vector3(1, 2, 3);
        g.transform.localRotation = new Quaternion(4, 5, 6,7);
        g.transform.localScale = new Vector3(8, 9, 10);
        GameObject g2 = SerializeObject(g);
        Assert.AreEqual(g.transform.localPosition, g2.transform.localPosition);
        Assert.AreEqual(g.transform.localRotation, g2.transform.localRotation);
        Assert.AreEqual(g.transform.localScale, g2.transform.localScale);
    }
    [TestMethod]
    void Memento()
    {
        SaveSystem.Memento s = new SaveSystem.Memento();
        s.id = 3;
        SaveSystem.Memento s2 = SerializeObject(s);
        Assert.AreEqual((int)s.id, (int)s2.id);
    }
    [TestMethod]
    void hasMomento()
    {
        GameObject g = GameObject.Find("Test");
        SaveSystem.Memento m = g.GetComponent<SaveSystem.HasMemento>().getMemento();
        Debug.Log(m);
    }

    T SerializeObject<T>(T t)
    {
        IFormatter formatter = SaveSystem.getFormatter();
        using (Stream s = new MemoryStream())
        {
            try
            {
                formatter.Serialize(s, t);
                s.Seek(0, SeekOrigin.Begin);
                T t2 = (T)formatter.Deserialize(s);
                return t2;
            }
            catch (SerializationException e)
            {
                Debug.Log(e.InnerException);
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
                Debug.Log(e.TargetSite);
                return default(T);
            }

        }
    }

}
