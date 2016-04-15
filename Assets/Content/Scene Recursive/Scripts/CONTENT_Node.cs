using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CONTENT_Node : MonoBehaviour 
{
    public enum Type
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Sigmoid,
        Tanh,
        Value,
    }
    public Type type;
    public List<CONTENT_Node> input;
    public List<CONTENT_Node> output;
    public ulong added;
}
