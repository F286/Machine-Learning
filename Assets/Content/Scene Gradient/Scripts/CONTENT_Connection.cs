using UnityEngine;
using System.Collections;

public class CONTENT_Connection : MonoBehaviour 
{
    public Node from;
    public Node to;

    public void Awake()
    {
        CONTENT_ManagerNeuron.instance.connections.Add(this);
    }

    public static void Create(Node a, Node b)
    {
        var c = a.gameObject.AddComponent<CONTENT_Connection>();
        c.from = a;
        c.to = b;
    }
}
