using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class Gate : MonoBehaviour
{
    public Gate inputA;
    public Gate inputB;

    public float value;
    public float gradient; //Derivative

    public virtual void Forward(Gate a, Gate b)
    {
        inputA = a;
        inputB = b;
    }
    public virtual void Backward()
    {
        
    }
    public void OnDrawGizmos()
    {
        var s = EditorStyles.whiteLargeLabel;
        s.richText = true;
        s.fontSize = 30;
        Handles.Label(transform.position, Display(), s);
    }
    public virtual string Display()
    {
        return "<color=red>" + value.ToString("F1") + "</color> <color=yellow>" + gradient.ToString("F1") + "</color>";
    }
}