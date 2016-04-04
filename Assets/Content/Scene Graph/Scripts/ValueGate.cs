using UnityEngine;
using System.Collections;

public class ValueGate : Gate
{
    protected override bool ShowGradient
    {
        get
        {
            return false;
        }
    }
//    public void Awake()
//    {
//        input = new Gate[1];
//    }
    public override void Forward(params Gate[] v)
    {
        base.Forward(v);
//        value = v[0].value;
    }
    public override void Backward()
    {
        
    }
    public override string Display()
    {
        return base.Display();
    }
}