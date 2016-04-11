using UnityEngine;
using System.Collections;

public class CONTENT_NodeAdd : Node
{
    public double _value;
    public double _derivative;
    public override double value { get { return _value; } set { _value = value; } }
    public override double derivative { get { return _derivative; } set { _derivative = value; } }

    public override void forward(params Node[] input)
    {
        value = input[0].value + input[1].value;
    }
    public override void backward(params Node[] input)
    {
        input[0].derivative += derivative;
        input[1].derivative += derivative;
    }
}