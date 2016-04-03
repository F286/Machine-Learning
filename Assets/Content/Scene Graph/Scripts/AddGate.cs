using UnityEngine;
using System.Collections;

public class AddGate : Gate
{
    public override void Forward(Gate a, Gate b)
    {
        base.Forward(a, b);
        value = a.value + b.value;
    }
    public override void Backward()
    {
        inputA.gradient += 1 * gradient;
        inputB.gradient += 1 * gradient;
    }
    public override string Display()
    {
        return "[+] " + base.Display();
    }
}