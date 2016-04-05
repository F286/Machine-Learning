using UnityEngine;
using System.Collections;
using UnityEditor;

public class NeuronGate : Gate
{
    ValueGate bias;
    ValueGate[] weights = new ValueGate[0];

    public void Start()
    {
        bias = gameObject.AddComponent<ValueGate>();
        bias.display = false;
//        bias.value = Random.Range(-0.1f, 0.1f);
        bias.value = Random.Range(-2f, 2f);

        weights = new ValueGate[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            weights[i] = gameObject.AddComponent<ValueGate>();
//            weights[i].value = Random.Range(-0.1f, 0.1f);
            weights[i].value = Random.Range(-0.5f, 0.5f);
            weights[i].display = false;
        }
//        parameter = new float[input.Length + 1];
//        parameterGradient = new float[input.Length + 1];
//
//        for (int i = 0; i < parameter.Length; i++)
//        {
//            parameter[i] = Random.Range(-0.5f, 0.5f);
//        }
    }
    float Calculate()
    {
        if (bias == null)
        {
            return 0;
        }
//        return bias.value + Mathf.Exp(
        var v = bias.value;
        for (int i = 0; i < weights.Length; i++)
        {
            v += input[i].value * weights[i].value;
        }
        v = 1 / (1 + Mathf.Exp(-v));
        v = (v - 0.5f) * 2f;
        return v;

//        var g = (2f * Mathf.Exp(v)) / (Mathf.Exp(v) + 1).Squared());
//        a = -1f / (a * a);

    }
    public override void Forward(params Gate[] v)
    {
        base.Forward(v);

        value = Calculate();

//        value = Calculate;
//        value = 1 / (1 + Mathf.Exp(-v[0].value));
    }
    public override void Backward()
    {
        if (bias == null)
        {
            return;
        }
        const float step = 0.1f;

        for (int i = 0; i < weights.Length; i++)
        {
            var orig = weights[i].value;
            var a = Calculate();
            weights[i].value += step;
            var b = Calculate();
            weights[i].value = orig;

            weights[i].gradient = (b - a) / step;
        }
        {
            var orig = bias.value;
            var a = Calculate();
            bias.value += step;
            var b = Calculate();
            bias.value = orig;

            bias.gradient = (b - a) / step;

            // analytical
//            var g = (2f * Mathf.Exp(bias.value)) / (Mathf.Exp(bias.value) + 1).Squared());
        }
        // TODO - try it first with numerical gradient calculation for deriviative, then use to compare against analytical gradient
//        var g = gradient;//1f / gradient;
//        g = -g;

        //        var 
//        var st = EditorStyles.whiteLabel;
//        st.richText = true;
//
//        for (int i = 0; i < weights.Length; i++)
//        {
//            var p = (transform.position + input[i].transform.position) / 2f - new Vector3(1f, -1f);
//
//            Handles.Label(p, weights[i].value.ToString("+#0.000;-#0.000") + " <color=yellow>" + weights[i].gradient.ToString("+#0.000;-#0.000") + "</color>", st);
//        }
//        Handles.Label(transform.position + new Vector3(0.5f, 0.5f), 
//            bias.value.ToString("+#0.000;-#0.000") + " <color=yellow>" + bias.gradient.ToString("+#0.000;-#0.000") + "</color>", st);
        //        if (parameter.Length > 0)
        //        {
        //            Handles.Label(transform.position + new Vector3(0.5f, 0.5f), parameter[0].ToString("+#0.000;-#0.000") + 
        //                " <color=yellow>" + parameterGradient[0].ToString("+#0.000;-#0.000") + "</color>", EditorStyles.whiteLabel);
        //        }

//        var total = 0f;
//        for (int i = 0; i < parameter.Length - 1; i++)
//        {
//            var s = 1 / (1 + Mathf.Exp(-input[i].value * parameter[i + 1]));
//            s = (s * (1 - s));
////            var sigGrad = (s * (1 - s)) * g;
////            var p = parameterGradient[i + 1] * sigGrad;
////            p = 5;
////            print(p);
////            p = parameterGradient[i + 1];
//            parameterGradient[i + 1] = s * gradient;
//            input[i].gradient += s * gradient;
//            total += s * gradient;
//        }
//        total /= (parameter.Length - 1);
//        parameterGradient[0] = total;
//        parameterGradient[0] = g;

//        input[0].gradient += (s * (1 - s)) * gradient;
    }
    public override void AddForce(float timeStep)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i].value += weights[i].gradient * timeStep;
//            weights[i] += parameterGradient[i] * timeStep;
        }
//        variables[i].value += force * variables[i].gradient;
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        var st = EditorStyles.whiteLabel;
        if (st != null)
        {
            st.richText = true;
        }

        for (int i = 0; i < weights.Length; i++)
        {
            var p = (transform.position + input[i].transform.position) / 2f - new Vector3(1f, -1f);

            Handles.Label(p, weights[i].value.ToString("+#0.000;-#0.000") + " <color=yellow>" + weights[i].gradient.ToString("+#0.000;-#0.000") + "</color>", st);
        }
        if (bias)
        {
            Handles.Label(transform.position + new Vector3(0.5f, 0.5f), 
                bias.value.ToString("+#0.000;-#0.000") + " <color=yellow>" + bias.gradient.ToString("+#0.000;-#0.000") + "</color>", st);
        }
        
//        var index = 0;
//        foreach (var item in input)
//        {
//            var a = transform.position + new Vector3(0.5f, -0.5f);
//            var b = item.transform.position + new Vector3(0.5f, -0.5f);
//            Debug.DrawLine(a, b, Color.gray);
//
//            if (index < weights.Length)
//            {
//                var st = EditorStyles.whiteLabel;
//                st.richText = true;
//                Handles.Label((a + b) / 2f - new Vector3(0.5f, -0.5f), parameter[index + 1].ToString("+#0.000;-#0.000") + 
//                    " <color=yellow>" + parameterGradient[index + 1].ToString("+#0.000;-#0.000") + "</color>", st);
//            }
//
//            index++;
//        }
//        if (parameter.Length > 0)
//        {
//            Handles.Label(transform.position + new Vector3(0.5f, 0.5f), parameter[0].ToString("+#0.000;-#0.000") + 
//                " <color=yellow>" + parameterGradient[0].ToString("+#0.000;-#0.000") + "</color>", EditorStyles.whiteLabel);
//        }
    }
    public override string Display()
    {
        return "[n] " + base.Display();
    }
}