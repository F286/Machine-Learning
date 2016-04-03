using UnityEngine;
using System.Collections;

public class MultiplyGate : Gate
{
    public override void Forward(Gate a, Gate b)
    {
        base.Forward(a, b);
        value = a.value * b.value;
    }
    public override void Backward()
    {
        inputA.gradient += inputB.value * gradient;
        inputB.gradient += inputA.value * gradient;
    }
    public override string Display()
    {
        return "[*] " + base.Display();
    }
}