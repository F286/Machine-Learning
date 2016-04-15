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
        Input,
    }
    public Type type;
    public float value;
    public float derivative;
    public List<CONTENT_Node> input;
    public List<CONTENT_Node> output;
    public ulong added;
    public List<CONTENT_Equation> equations;
    public int index;
}
