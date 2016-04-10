using UnityEngine;
using System.Collections;

public class CONTENT_ValueNode : INode
{
    public double _value;
    public double _derivative;
    public override double value { get { return _value; } set { _value = value; } }
    public override double derivative { get { return _derivative; } set { _derivative = value; } }

    public override void forward(params INode[] input)
    {

    }
    public override void backward(params INode[] input)
    {

    }
//    public ValueNode(float value)
//    {
//        this.value = value;
//        this.derivative = 0;
//    }
}