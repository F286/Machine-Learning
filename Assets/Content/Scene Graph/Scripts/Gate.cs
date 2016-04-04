using UnityEngine;
using System.Collections;
using UnityEditor;

public abstract class Gate : MonoBehaviour
{
    public Gate[] input;

    public float value;
    public float gradient; //Derivative

    protected virtual bool ShowGradient
    {
        get
        {
            return true;
        }
    }

    public virtual void Forward(params Gate[] v)
    {
        input = v;
    }
    public virtual void Backward()
    {
        
    }
    public void OnDrawGizmos()
    {
        foreach (var item in input)
        {
            Debug.DrawLine( transform.position + new Vector3(0.5f, -0.5f), 
                            item.transform.position + new Vector3(0.5f, -0.5f), Color.gray);
        }
            
        var s = EditorStyles.whiteLargeLabel;
        s.richText = true;
        s.fontSize = 20;
        Handles.Label(transform.position, Display(), s);
    }
    public virtual string Display()
    {
        var s = "";
        if (ShowGradient)
        {
            s = "<color=red>" + value.ToString("F1") + "</color>" + " <color=yellow>" + gradient.ToString("F1") + "</color>";
        }
        else
        {
            s = "<color=grey>" + value.ToString("F1") + "</color>";
        }
        return s;
    }
}