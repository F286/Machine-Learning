using UnityEngine;
using System.Collections;

public abstract class Node : MonoBehaviour
{
    public abstract double value { get; set; }
    public abstract double derivative { get; set; }

//    INode[] input { get; }

    public abstract void forward(params Node[] input);
    public abstract void backward(params Node[] input);

    public void Awake()
    {
        CONTENT_NeuronManager.instance.nodes.Add(this);
    }
}