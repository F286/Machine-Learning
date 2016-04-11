using UnityEngine;
using System.Collections;

public class CONTENT_Neuron : MonoBehaviour 
{
    public Node input; 
    public Node output;

    public void Awake()
    {
//        gameObject.AddComponent<CONTENT_ValueNode>();

//        var threadValue = gameObject.AddComponent<CONTENT_ValueNode>();
//        var threadMultiply = gameObject.AddComponent<CONTENT_MultiplyNode>();

        var add = gameObject.AddComponent<CONTENT_NodeAdd>();
        add.display = gameObject.AddComponent<CONTENT_Display>();

        var sig = gameObject.AddComponent<CONTENT_NodeSigmoid>();
        CONTENT_Connection.Create(add, sig);

//        var multiplyConnect

        input = add;
        output = sig;


    }
}
