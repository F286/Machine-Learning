using UnityEngine;
using System.Collections;

public class CONTENT_ConnectionWeighted : MonoBehaviour 
{
    public Node input; 
    public Node output;
    public void Awake()
    {
        var threadValue = gameObject.AddComponent<CONTENT_NodeValue>();
        var threadMultiply = gameObject.AddComponent<CONTENT_NodeMultiply>();

        CONTENT_Connection.Create(threadValue, threadMultiply);

        input = threadMultiply;
        output = threadMultiply;
    }
}
