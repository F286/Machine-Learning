using UnityEngine;
using System.Collections;

[System.Serializable]
public struct DataPointer
{
    public int frame;
    public int node;

    public double value
    {
        get
        {
            return CONTENT_NodeManager.instance.frames[frame].value[node];
        }
        set
        {
            CONTENT_NodeManager.instance.frames[frame].value[node] = value;
        }
    }
    public double derivative
    {
        get
        {
            return CONTENT_NodeManager.instance.frames[frame].derivative[node];
        }
        set
        {
            CONTENT_NodeManager.instance.frames[frame].derivative[node] = value;
        }
    }
    public DataPointer(int frame, int node)
    {
        this.frame = frame;
        this.node = node;
    }
}