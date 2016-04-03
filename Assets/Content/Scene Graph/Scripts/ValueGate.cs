using UnityEngine;
using System.Collections;

public class ValueGate : Gate
{
//    public void Forward(Gate a)
//    {
//        Forward(a, null);
//    }
    public override void Forward(Gate a, Gate b)
    {
        base.Forward(a, null);
        value = a.value;
    }
    public override void Backward()
    {
        
    }
    public override string Display()
    {
        return base.Display();
    }
}