using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CONTENT_NeuronManager : MonoBehaviour 
{
    const float NumericalStepSize = 0.00001f;

//    public double[] input;
//    public double[] weights;
    public float bias;
    public Vector4 input;
    public Vector4 weights;

    public float value;
    public float analyticalGradient;
    public float numericalGradient;

    public static Vector4 TestAxis = new Vector4(1, 0, 0, 0);

    public INode[] nodes;
    public CONTENT_Connection[] connections;

    public List<int[]> leftToRight = new List<int[]>();

    public void Start()
    {
        nodes = gameObject.GetComponentsInChildren<INode>();
        connections = gameObject.GetComponentsInChildren<CONTENT_Connection>();

        AddConnections(GameObject.FindGameObjectWithTag("output").GetComponent<INode>());

//        print(leftToRight.Count);
//        print(leftToRight[0]);
//        print(leftToRight[0][0]);
//        print(leftToRight[0][1]);
    }

    List<INode> alreadyAdded = new List<INode>();
    void AddConnections(INode node)
    {
        if (!alreadyAdded.Contains(node))
        {
            print(node);
            alreadyAdded.Add(node);
//            nodes.Add(node);
            var inputs = new List<int>();
            foreach (var item in GetInputs(node))
            {
                print(GetIndex(item));
                inputs.Add(GetIndex(item));
                AddConnections(item);
            }
            leftToRight.Add(inputs.ToArray());
        }
//        connections = 
    }
    public IEnumerable<INode> GetInputs(INode to)
    {
        foreach (var item in connections)
        {
            if (item.to == to)
            {
                yield return item.from;
            }
        }
    }
    public int GetIndex(INode item)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void Update()
    {
        value = Forward(input, weights);
        analyticalGradient = Backward(input, weights).MaskSum(TestAxis);
        numericalGradient = BackwardNumerical(input, weights);

        Debug.DrawLine(new Vector3(-5, 0, 0), new Vector3(5, 0, 0), Color.black);
        Debug.DrawLine(new Vector3(0, -5, 0), new Vector3(0, 5, 0), Color.black);

        var g = Color.green;
        g.a = 0.8f;
        var r = Color.red;
        r.a = 0.8f;

//        print(input.InverseMask(TestAxis));
        for (float i = -5; i < 5; i += 0.1f)
        {
            var a = input.InverseMask(TestAxis) + TestAxis * i;
            var b = input.InverseMask(TestAxis) + TestAxis * (i + 0.1f);

            Debug.DrawLine(new Vector3(i, Forward(a, weights)), new Vector3(i + 0.1f, Forward(b, weights)), Color.black);
            Debug.DrawLine(new Vector3(i, BackwardNumerical(a, weights)), new Vector3(i + 0.1f, BackwardNumerical(b, weights)), g);
            Debug.DrawLine(new Vector3(i, Backward(a, weights).MaskSum(TestAxis)), new Vector3(i + 0.1f, Backward(b, weights).MaskSum(TestAxis)), r);
        }
    }

    public static float Forward(Vector4 input, Vector4 weights)
    {
        return 1;
//        var i0 = new ValueNode(input.x);
//        var i1 = new ValueNode(input.y);
//        var w0 = new ValueNode(weights.x);
//        var w1 = new ValueNode(weights.y);
//
//        var m0 = new MultiplyNode();
//        var m1 = new MultiplyNode();
//
//        var total = new AddNode();
//        total.derivative = 1;
//
//        m0.forward(i0, w0);
//        m1.forward(i1, w1);
//
//        total.forward(m0, m1);
//
//        return (float)total.value;

//        float value = 0;
//        // apply weights and add inputs
//        value = input.x * weights.x + input.y * weights.y + input.z * weights.z + input.w;//weights.w;
//
//        // easing curve between -1 and 1
//        value = (float)System.Math.Tanh(value);

//        return value;
    }
    public static Vector4 Backward(Vector4 input, Vector4 weights)
    {
        return Vector4.one;
//        var i0 = new ValueNode(input.x);
//        var i1 = new ValueNode(input.y);
//        var w0 = new ValueNode(weights.x);
//        var w1 = new ValueNode(weights.y);
//
//        var m0 = new MultiplyNode();
//        var m1 = new MultiplyNode();
//
//        var total = new AddNode();
//        total.derivative = 1;
//
//        m0.forward(i0, w0);
//        m1.forward(i1, w1);
//
//        total.forward(m0, m1);
//
//        total.backward(m0, m1);
//        m1.backward(i1, w1);
//        m0.backward(i0, w0);

//        return new Vector4((float)i0.derivative, (float)i1.derivative, 0, 0);
//        Vector4 gradient = Vector4.one;
//
//        // apply weights and add inputs
//        gradient.x *= weights.x;
//        gradient.y *= weights.y;
//        gradient.z *= weights.z;
//        gradient.w *= 1;
//
//        // calculate what value was right before function (can cache this)
//        var value = input.x * weights.x + input.y * weights.y + input.z * weights.z + input.w;//weights.w;
//
//        // easing curve between -1 and 1
//        gradient *= 1 - ((float)System.Math.Tanh(value)).Squared();
//
//        return gradient;
    }
    public static float BackwardNumerical(Vector4 input, Vector4 weights)
    {
        var a = Forward(input, weights);
        var b = Forward(input + NumericalStepSize * TestAxis, weights);
        return (b - a) / NumericalStepSize;
    }

    public void OnGUI()
    {
        if (GUILayout.Button("X"))
        {
            TestAxis = new Vector4(1, 0, 0, 0);
        }
        if (GUILayout.Button("Y"))
        {
            TestAxis = new Vector4(0, 1, 0, 0);
        }
        if (GUILayout.Button("Z"))
        {
            TestAxis = new Vector4(0, 0, 1, 0);
        }
        if (GUILayout.Button("W"))
        {
            TestAxis = new Vector4(0, 0, 0, 1);
        }
    }
}
