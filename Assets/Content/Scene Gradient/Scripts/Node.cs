using UnityEngine;
using System.Collections;

public abstract class Node : MonoBehaviour
{
    public abstract double value { get; set; }
    public abstract double derivative { get; set; }

//    INode[] input { get; }

    public abstract void forward(params Node[] input);
    public abstract void backward(params Node[] input);
    public abstract void train(float step);

    public CONTENT_Display display;
    public Node[] DebugInputs;

    public bool CreateDisplay;

    public void Awake()
    {
        CONTENT_ManagerNeuron.instance.nodes.Add(this);
        if (CreateDisplay)
        {
            display = gameObject.AddComponent<CONTENT_Display>();
        }
    }
    public void LateUpdate()
    {
        if(display)
        {
            display.value = (float)value;
            display.derivative = (float)derivative;
        }
    }
}