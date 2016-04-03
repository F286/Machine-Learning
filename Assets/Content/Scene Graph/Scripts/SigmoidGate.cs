using UnityEngine;
using System.Collections;

public class SigmoidGate : Gate
{
//    public void Forward(Gate a)
//    {
//        Forward(a, null);
//    }
    public override void Forward(Gate a, Gate b)
    {
        base.Forward(a, b);
//        print(a.value);
        value = 1 / (1 + Mathf.Exp(-a.value));
    }
    public override void Backward()
    {
        var s = 1 / (1 + Mathf.Exp(-inputA.value));
        inputA.gradient += (s * (1 - s)) * gradient;
    }
    public override string Display()
    {
        return "[s] " + base.Display();
    }
}