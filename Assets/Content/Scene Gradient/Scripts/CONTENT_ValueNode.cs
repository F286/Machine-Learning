using UnityEngine;
using System.Collections;

public class CONTENT_ValueNode : Node
{
    public double _value;
    public double _derivative;
    public override double value { get { return _value; } set { _value = value; } }
    public override double derivative { get { return _derivative; } set { _derivative = value; } }

    public override void forward(params Node[] input)
    {

    }
    public override void backward(params Node[] input)
    {

    }
//    public ValueNode(float value)
//    {
//        this.value = value;
//        this.derivative = 0;
//    }
}