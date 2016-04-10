using UnityEngine;
using System.Collections;

public class CONTENT_Connection : MonoBehaviour 
{
    public Node from;
    public Node to;

    public void Awake()
    {
        CONTENT_NeuronManager.instance.connections.Add(this);
    }
}
