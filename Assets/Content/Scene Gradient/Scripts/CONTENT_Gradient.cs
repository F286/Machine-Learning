using UnityEngine;
using System.Collections;

public class CONTENT_Gradient : MonoBehaviour 
{
    const float NumericalStepSize = 0.001f;

    public float input;

    public float value;
    public float analyticalGradient;
    public float numericalGradient;

    public void Update()
    {
        value = Forward(input);
        analyticalGradient = Backward(input);
        numericalGradient = BackwardNumerical(input);

        Debug.DrawLine(new Vector3(-5, 0, 0), new Vector3(5, 0, 0), Color.black);
        Debug.DrawLine(new Vector3(0, -5, 0), new Vector3(0, 5, 0), Color.black);

        for (float i = -5; i < 5; i += 0.1f)
        {
            Debug.DrawLine(new Vector3(i, Forward(i)), new Vector3(i + 0.1f, Forward(i + 0.1f)), Color.blue);
            Debug.DrawLine(new Vector3(i, BackwardNumerical(i)), new Vector3(i + 0.1f, BackwardNumerical(i + 0.1f)), Color.green);
            Debug.DrawLine(new Vector3(i, Backward(i)), new Vector3(i + 0.1f, Backward(i + 0.1f)), Color.red);
        }
    }

    public static float Forward(float input)
    {
        return input * input * input * input;
//        return input * input;
//        return 1 / (input * 4);
    }
    public static float Backward(float input)
    {
        return 4 * input * input * input;
//        return 2 * input;
//        return -1 / (input.Squared() * 4);
    }
    public static float BackwardNumerical(float input)
    {
        var a = Forward(input);
        var b = Forward(input + NumericalStepSize);
        return (b - a) / NumericalStepSize;
    }
}
