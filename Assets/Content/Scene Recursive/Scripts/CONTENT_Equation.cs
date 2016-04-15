using UnityEngine;
using System.Collections;

public class CONTENT_Equation : MonoBehaviour 
{
    public CONTENT_Node.Type type;
    public CONTENT_Equation[] input;
    public DataPointer[] In;
    public DataPointer Val;
//    public DataPointer Out;
    public double value;
//    public dta

    public void forward()
    {
        Val.value = 0;
        switch (type)
        {
            case CONTENT_Node.Type.Add:
                break;
            case CONTENT_Node.Type.Subtract:
                break;
            case CONTENT_Node.Type.Multiply:
                break;
            case CONTENT_Node.Type.Divide:
                break;
            case CONTENT_Node.Type.Sigmoid:
                break;
            case CONTENT_Node.Type.Tanh:
                break;
            case CONTENT_Node.Type.Value:
                break;
            case CONTENT_Node.Type.Input:
                break;
        }
    }
    public void backward()
    {
        
    }
    public void train(float step)
    {

    }
}
