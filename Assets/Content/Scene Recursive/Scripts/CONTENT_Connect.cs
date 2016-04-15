using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(CONTENT_Node))]
public class CONTENT_Connect : MonoBehaviour 
{
    public CONTENT_Node from;

    public void Awake()
    {
        Assert.IsTrue(from != null, "on CONTENT_Connect 'from' must be set.");

        gameObject.GetComponent<CONTENT_Node>().input.Add(from);
        from.output.Add(gameObject.GetComponent<CONTENT_Node>());
    }
}
