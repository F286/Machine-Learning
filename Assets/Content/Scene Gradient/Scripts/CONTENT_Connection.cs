using UnityEngine;
using System.Collections;

public class CONTENT_Connection : MonoBehaviour 
{
    public Node from;
    public Node to;

    public CONTENT_DisplayConnection display;

    public void Awake()
    {
        CONTENT_ManagerNeuron.instance.connections.Add(this);
    }
    public static void Create(Node a, Node b, bool createDisplay = false)
    {
        var g = new GameObject("connection");

        var c = g.AddComponent<CONTENT_Connection>();
        c.from = a;
        c.to = b;

        if (createDisplay)
        {
            var d = g.AddComponent<CONTENT_DisplayConnection>();
            d.from = a;
            d.to = b;
            c.display = d;
        }
    }
    public void LateUpdate()
    {
        if(display)
        {
            display.value = 0;
//            display.value = 0.5f;
            display.derivative = 0;
        }
    }
}
