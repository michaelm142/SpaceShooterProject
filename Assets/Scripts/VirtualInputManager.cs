using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualInputManager : MonoBehaviour
{
    public List<VirtualInput> axes;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            throw new System.InvalidOperationException("More than one instance of Virtual Input Manager");

        instance = this;
        Input.multiTouchEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var x in axes)
            Debug.Log(string.Format("{0}|{1}", x.name, x.x.Value));
    }

    public static Vector2 GetAxis(string name)
    {
        VirtualInput i = instance.axes.Find(a => a.name == name);

        if (i == null)
        {
            Debug.LogWarning(string.Format("Unknown virtual axis: {0}", name));
            return Vector2.zero;
        }

        return i.x.Value;
    }

    private static VirtualInputManager instance;
}

public abstract class VirtualInputAxis : MonoBehaviour
{
    public Vector2 Value { get; protected set; }
}

[System.Serializable]
public class VirtualInput
{
    public string name;

    public VirtualInputAxis x;
}
