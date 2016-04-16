using UnityEngine;
using System.Collections;

public class CONTENT_Equation : MonoBehaviour 
{
    public CONTENT_Node.Type type;
//    public CONTENT_Equation[] input;
    public DataPointer[] In;
    public DataPointer Val;
//    public double value;

    public void forward()
    {
        switch (type)
        {
            case CONTENT_Node.Type.Add:
                Val.value = 0;
                for (int i = 0; i < In.Length; i++)
                {
                    Val.value += In[i].value;
                }
                break;
            case CONTENT_Node.Type.Subtract:
                break;
            case CONTENT_Node.Type.Multiply:
                Val.value = 1;
                for (int i = 0; i < In.Length; i++)
                {
                    Val.value *= In[i].value;
                }
                break;
            case CONTENT_Node.Type.Divide:
                break;
            case CONTENT_Node.Type.Sigmoid:
                Val.value = 0;
                for (int i = 0; i < In.Length; i++)
                {
                    Val.value += In[i].value;
                }
                Val.value = Core.Sigmoid(Val.value);
                break;
            case CONTENT_Node.Type.Tanh:
                Val.value = 0;
                for (int i = 0; i < In.Length; i++)
                {
                    Val.value += In[i].value;
                }
                Val.value = System.Math.Tanh(Val.value);
                break;
            case CONTENT_Node.Type.Value:
                break;
            case CONTENT_Node.Type.Input:
                break;
        }
    }
    public void backward()
    {
        switch (type)
        {
            case CONTENT_Node.Type.Add:
                for (int i = 0; i < In.Length; i++)
                {
                    In[i].derivative += Val.derivative;
                }
//                for (int i = 0; i < In.Length; i++)
//                {
//                    Val.value += In[i].value;
//                }
//                Val.value = 0;
//                for (int i = 0; i < In.Length; i++)
//                {
//                    Val.value += In[i].value;
//                }
                break;
            case CONTENT_Node.Type.Subtract:
                break;
            case CONTENT_Node.Type.Multiply:
                for (int a = 0; a < In.Length; a++)
                {
                    var d = Val.derivative;
                    for (int b = 0; b < In.Length; b++)
                    {
                        if (a != b)
                        {
                            d *= In[b].value;
                        }
                    }
//                    print(d);
                    In[a].derivative += d;
                }
//                Val.value = 1;
//                for (int i = 0; i < In.Length; i++)
//                {
//                    Val.value *= In[i].value;
//                }
                break;
            case CONTENT_Node.Type.Divide:
                break;
            case CONTENT_Node.Type.Sigmoid:
//                Val.value = 0;
//                for (int i = 0; i < In.Length; i++)
//                {
//                    Val.value += In[i].value;
//                }
//                Val.value = Core.Sigmoid(Val.value);
                break;
            case CONTENT_Node.Type.Tanh:
//                Val.value = 0;
//                for (int i = 0; i < In.Length; i++)
//                {
//                    Val.value += In[i].value;
//                }
//                Val.value = System.Math.Tanh(Val.value);
                break;
            case CONTENT_Node.Type.Value:
                break;
            case CONTENT_Node.Type.Input:
                break;
        }
    }
    public void train(float step)
    {

    }

    [Header("Debug")]
    public double debugValue;
    public double debugDerivative;

    public void LateUpdate()
    {
        debugValue = Val.value;
        debugDerivative = Val.derivative;
    }
}
