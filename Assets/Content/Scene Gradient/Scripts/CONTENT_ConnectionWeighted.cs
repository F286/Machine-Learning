﻿using UnityEngine;
using System.Collections;

public class CONTENT_ConnectionWeighted : MonoBehaviour 
{
    public Node input; 
    public Node output;
    public void Awake()
    {
        var threadValue = gameObject.AddComponent<CONTENT_NodeValue>();
        threadValue.value = 1f;
        var threadMultiply = gameObject.AddComponent<CONTENT_NodeMultiply>();

        CONTENT_Connection.Create(threadValue, threadMultiply);

        input = threadMultiply;
        output = threadMultiply;
    }
    public static void Create(Node a, Node b)
    {
        var g = new GameObject("connection weighted");

        var c = g.AddComponent<CONTENT_ConnectionWeighted>();
        c.input = a;
        c.output = b;

        var d = g.AddComponent<CONTENT_DisplayConnection>();
        d.from = a;
        d.to = b;
        c.GetComponent<CONTENT_NodeValue>().display = d;
    }
}
