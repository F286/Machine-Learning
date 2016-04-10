using UnityEngine;
using System.Collections;

public abstract class INode : MonoBehaviour
{
    public abstract double value { get; set; }
    public abstract double derivative { get; set; }

//    INode[] input { get; }

    public abstract void forward(params INode[] input);
    public abstract void backward(params INode[] input);
}