using UnityEngine;
using System.Collections;

public class CONTENT_Gradient : MonoBehaviour 
{
    const float NumericalStepSize = 0.00001f;

    public Vector4 input;

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

        var g = Color.green;
        g.a = 0.8f;
        var r = Color.red;
        r.a = 0.8f;

        for (float i = -5; i < 5; i += 0.1f)
        {
            var a = new Vector4(i, 1, 0, 0);
            var b = new Vector4(i + 0.1f, 1, 0, 0);
            Debug.DrawLine(new Vector3(i, Forward(a)), new Vector3(i + 0.1f, Forward(b)), Color.black);
            Debug.DrawLine(new Vector3(i, BackwardNumerical(a)), new Vector3(i + 0.1f, BackwardNumerical(b)), g);
            Debug.DrawLine(new Vector3(i, Backward(a)), new Vector3(i + 0.1f, Backward(b)), r);
        }
    }

    public static float Forward(Vector4 input)
    {
//        Vector4 value = Vector4.zero;
        float value = 0;
        // apply weights and add inputs
        value = input.x * input.x + input.y * 0.5f + input.z * 0.3f;

        // easing curve between -1 and 1
        value = (float)System.Math.Tanh(value);

        return value;
    }
    public static float Backward(Vector4 input)
    {
        Vector4 gradient = Vector4.one;

        // apply weights and add inputs
        gradient.x *= 2 * input.x;

        // calculate what value was right before tanh function (can cache this)
        var value = input.x * input.x + input.y * 0.5f + input.z * 0.3f;

        // easing curve between -1 and 1
        gradient.x *= 1 - ((float)System.Math.Tanh(value)).Squared();

        return gradient.x;
    }
//    public static float Forward(float value)
//    {
//        value *= 2;
//        value = (Mathf.Exp(value) - Mathf.Exp(-value)) / (Mathf.Exp(value) + Mathf.Exp(-value));
//        return value;
//    }
//    public static float Backward(float gradient)
//    {
//        gradient *= 2;
//        gradient = 2 / (Mathf.Exp(gradient) + Mathf.Exp(-gradient));
//        gradient *= gradient;
//        gradient *= 2;
//        return gradient;
//    }
    public static float BackwardNumerical(Vector4 input)
    {
        var a = Forward(input);
        var b = Forward(input + new Vector4(NumericalStepSize, 0));
        return (b - a) / NumericalStepSize;
    }
}
