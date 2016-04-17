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
    [System.Serializable]
    public class SetInput
    {
        public int frame = 0;
        public float value;
    }
    [Header("Basic")]
    public Type type;
    public double value;
    public double[] derivative = new double[0];
    public SetInput[] setInput; 
    [Header("Advanced")]
    public List<CONTENT_Node> input = new List<CONTENT_Node>();
    public List<CONTENT_Node> output = new List<CONTENT_Node>();
//    [HideInInspector()]
    public List<CONTENT_Equation> equations = new List<CONTENT_Equation>();
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
        if (derivative.Length != CONTENT_NodeManager.TotalFrames)
        {
            derivative = new double[CONTENT_NodeManager.TotalFrames];
        }
        for (int i = 0; i < derivative.Length; i++)
        {
            var s = current;
            s.frame = i;
            derivative[i] = s.derivative;
        }
//        derivative = current.derivative;
        
    }
}
