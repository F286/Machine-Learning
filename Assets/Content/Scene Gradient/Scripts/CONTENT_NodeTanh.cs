using UnityEngine;
using System.Collections;

public class CONTENT_NodeTanh : Node
{
    public double _value;
    public double _derivative;
    public override double value { get { return _value; } set { _value = value; } }
    public override double derivative { get { return _derivative; } set { _derivative = value; } }

    public override void forward(params Node[] input)
    {
        value = System.Math.Tanh(input[0].value);
    }
    public override void backward(params Node[] input)
    {
        var fD = 1 - (System.Math.Tanh(input[0].value)).Squared();
        input[0].derivative += fD * derivative;

//        var s = 1 / (1 + System.Math.Exp(-input[0].value));
//        input[0].derivative += s * (1 - s) * derivative;
    }
    public override void train(float step)
    {
        
    }
}