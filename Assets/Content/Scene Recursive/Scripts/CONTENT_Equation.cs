using UnityEngine;
using System.Collections;

public class CONTENT_Equation : MonoBehaviour 
{
    public CONTENT_Node.Type type;
    public CONTENT_Equation[] input;

    public void forward(params CONTENT_Equation[] input)
    {
        switch (type)
        {
            case CONTENT_Node.Type.Add:
                break;
        }
    }
    public void backward(params CONTENT_Equation[] input)
    {
        
    }
    public void train(float step)
    {

    }
}
