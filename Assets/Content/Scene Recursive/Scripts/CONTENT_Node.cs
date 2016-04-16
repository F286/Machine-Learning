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
    [Header("Basic")]
    public Type type;
    public double value;
    public double derivative;
    [Header("Advanced")]
    public List<CONTENT_Node> input;
    public List<CONTENT_Node> output;
//    [HideInInspector()]
    public List<CONTENT_Equation> equations;
//    public int index;
    public DataPointer current;
    [Header("Internal")]
    public ulong addedBitmask;

    public void LateUpdate()
    {
        if (type != Type.Input)
        {
            value = current.value;
        }
        derivative = current.derivative;
        
    }
}
