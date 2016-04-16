using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_NodeManager : MonoBehaviour
{
    public const int TotalFrames = 1;

    [System.Serializable]
    public class Frame
    {
        public double[] value;
        public double[] derivative;

        public Frame(int length)
        {
            value = new double[length];
            derivative = new double[length];
        }
    }

    public List<CONTENT_Node> nodes;
    public List<CONTENT_Node> inputs;
//    public CONTENT_Node[] nodes;
    public Frame[] frames;
    public List<CONTENT_Equation> equations;
    public Gradient gradient;

    public void Start()
    {
//        nodes = GameObject.FindObjectsOfType<CONTENT_Node>();
//        var connect = GameObject.FindObjectsOfType<CONTENT_Connect>();

        AddEquations(GameObject.FindWithTag("output").GetComponent<CONTENT_Node>());

        frames = new Frame[TotalFrames];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame(nodes.Count);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            var item = nodes[i];
            var v = item.gameObject.AddComponent<CONTENT_NodeVisualize>();
            item.index = i;
        }
    }

    CONTENT_Equation AddEquations(CONTENT_Node node, int age = 0, List<CONTENT_Node> currentTree = null)
    {
        if (age >= TotalFrames)
        {
            return null;
        }
        if (currentTree == null)
        {
            currentTree = new List<CONTENT_Node>();
        }
        currentTree.Add(node);
//        CONTENT_Equation[] input = new CONTENT_Equation[node.input.Count];
        List<CONTENT_Equation> input = new List<CONTENT_Equation>();
        for (int i = 0; i < node.input.Count; i++)
        {
            var item = node.input[i];
            if ((item.added & 1U << age) == 0)
            {
                item.added |= 1U << age;
                CONTENT_Equation add = null;
                if (currentTree.Contains(item))
                {
                    // this node is recursive, increase age and reset tree
                    add = AddEquations(item, age + 1, null);
                }
                else
                {  
                    add = AddEquations(item, age, currentTree);
                }
                if (add != null)
                {
                    input.Add(add);
                }
            }
        }
        if (!nodes.Contains(node))
        {
            nodes.Add(node);
        }
        var g = new GameObject(node.name + " (" + node.type.ToString() + ")");
        var e = g.AddComponent<CONTENT_Equation>();
        e.type = node.type;
        e.input = input.ToArray();
//        e.age = age;
        e.Val = new DataPointer(age, node.index);
        var _in = new DataPointer[e.input.Length];
        for (int i = 0; i < _in.Length; i++)
        {
            _in[i] = e.input[i].Val;
        }
//        e.Out = node.
        equations.Add(e);
        node.equations.Add(e);
        if (age == 0 && node.type == CONTENT_Node.Type.Input)
        {
            inputs.Add(node);
        }
        return e;
    }
    bool hasUpdated = false;
    public void Update()
    {
        if (!hasUpdated)
        {
            hasUpdated = true;

            for (int f = 0; f < frames.Length; f++)
            {
                for (int n = 0; n < nodes.Count; n++)
                {
                    frames[f].derivative[n] = 0;
                    if (nodes[n].type == CONTENT_Node.Type.Input)
                    {
                        //frames[f].value[n] = 
                    }
                }
            }
            for (int i = 0; i < inputs.Count; i++)
            {
                frames[0].value[inputs[i].index] = inputs[i].value;
//                frames[TotalFrames - 3].value[inputs[i].index] = inputs[i].value / 2f;
//                frames[TotalFrames - 4].value[inputs[i].index] = inputs[i].value / -2f;
            }
            for (int i = 0; i < equations.Count; i++)
            {
                var e = equations[i];
                e.forward();//ref frames[e.valueP.frame].value[e.valueP.node]);
//                e.forward(ref frames[e.age].value[e.);
            }
            for (int i = equations.Count - 1; i >= 0; i--)
            {
                equations[i].backward();
            }
        }
    }

    static CONTENT_NodeManager _inst;
    public static CONTENT_NodeManager instance
    {
        get
        {
            if(_inst == null)
            {
                _inst = GameObject.FindObjectOfType<CONTENT_NodeManager>();
            }
            return _inst;
        }
    }
}
