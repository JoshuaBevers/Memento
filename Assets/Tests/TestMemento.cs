using UnityEngine;
using System.Collections;

public class TestMemento : MonoBehaviour, SaveSystem.HasMemento{

    
    public SaveSystem.Memento getMemento()
    {
        SaveSystem.Memento m = new SaveSystem.Memento();
        m.vals.Add("localPosition", transform.localPosition);
        return m;
    }

    public void LoadMemento(SaveSystem.Memento s)
    {
        transform.localPosition = (Vector3)s.vals["localPosition"];
    }
}
